using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Redis
{
	public interface IRedisService
	{
		void Connect();
		IDatabase GetDb(int db);
	}
}
