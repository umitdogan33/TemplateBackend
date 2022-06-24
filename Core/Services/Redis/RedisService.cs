using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Redis
{
	public class RedisService : IRedisService
	{
		ConnectionMultiplexer connectionMultiplexer;

		public RedisService()
		{
			Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
		}

		protected IConfiguration Configuration { get; }

		public void Connect()
		{
			connectionMultiplexer = ConnectionMultiplexer.Connect(Configuration.GetConnectionString(Configuration.GetConnectionString("Redis")));
		}

		public IDatabase GetDb(int db) {return connectionMultiplexer.GetDatabase(db); 
		}
	}
}
