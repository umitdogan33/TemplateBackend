using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.logging.Serilog;
using Core.Services.Redis;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<IRedisService, RedisService>();
            serviceCollection.AddSingleton<IFileLogger, FileLogger>();
            serviceCollection.AddSingleton<IMSqlServerLogger, MSqlServerLogger>();
            serviceCollection.AddSingleton<ISeqLogger, SeqLogger>();
            serviceCollection.AddSingleton<IBaseLogger, BaseLogger>();
            serviceCollection.AddSingleton<Stopwatch>();
         
        }
    }
}
