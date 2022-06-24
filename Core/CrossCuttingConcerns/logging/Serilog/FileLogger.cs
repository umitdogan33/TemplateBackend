using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.CrossCuttingConcerns.logging.Serilog
{
    public class FileLogger : IFileLogger
    {
        public void Configure(string className)
        {

            //var ClassName = fullName.Substring(19, fullName.Length - 26);

            var path = Environment.CurrentDirectory;
            IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .MinimumLevel.Information()
         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Fatal)
                    .WriteTo.File(path + @"\\" + @"logs" +@"\\"+ @className + @".txt")
                    .CreateLogger();
        }
    }
}
