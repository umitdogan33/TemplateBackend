using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.CrossCuttingConcerns.logging.Serilog
{
    public class SeqLogger : ISeqLogger
    {
        public void Configure(string className)
        {

            var path = Environment.CurrentDirectory;
            IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .MinimumLevel.Information()
         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Fatal)
                    .WriteTo.File(path + @"\\" + @className + @".txt")
                    .CreateLogger();
        }
    }
}
