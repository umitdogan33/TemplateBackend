using StackExchange.Redis;
using System.Collections.Generic;

namespace Core.DataAccess.Redis
{
	public interface IRedisEntityRepository<TEntity>
	{
		public void Add(TEntity product);

		public void Delete(TEntity product);

		public List<RedisValue> GetAll();

		public void Update(TEntity product);

	}
}