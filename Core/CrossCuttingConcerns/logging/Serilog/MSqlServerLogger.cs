using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.CrossCuttingConcerns.logging.Serilog
{
    public class MSqlServerLogger : IMSqlServerLogger
    {
        public void Configure()
        {
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            Log.Logger = new LoggerConfiguration()
               .WriteTo.MSSqlServer(
                   connectionString: "Server=(localdb)\\MSSQLLocalDB;Database=ECommerce;Trusted_Connection=True",
                   tableName: "logs",
                   appConfiguration: configuration,
                   autoCreateSqlTable: true,
                   schemaName: "dbo"
               ).CreateLogger();
        }
    }
}
