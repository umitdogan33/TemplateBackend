using Core.Services.Redis;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;

namespace Core.DataAccess.Redis
{
	public class RedisEntityRepositoryBase<TEntity> : IRedisEntityRepository<TEntity>
	{
		IRedisService _redisService;
		IDatabase database;
		string _keyName;
		public RedisEntityRepositoryBase(int dbNumber,string keyName)
		{
			_redisService = ServiceTool.ServiceProvider.GetService<IRedisService>(); ;
			database = _redisService.GetDb(dbNumber);
			_keyName = keyName;
		}

		public void Add(TEntity product)
		{
			var jsonObject = JsonConvert.SerializeObject(product);
			database.ListLeftPush(_keyName , jsonObject);
		}

		public void Delete(TEntity product)
		{
			throw new NotImplementedException();
		}

		public List<RedisValue> GetAll()
		{
			RedisValue[] values = database.ListRange(_keyName);
			//int count = 1;
			return values.ToList();
			//values.ToList().ForEach(o => Console.WriteLine($"{o}"));
			//return values.ToList();
			//Console.WriteLine(values);
		}

			public void Update(TEntity product)
		{
			throw new NotImplementedException();
		}
	}
}
