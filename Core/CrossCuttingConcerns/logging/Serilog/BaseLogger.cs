using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace Core.CrossCuttingConcerns.logging.Serilog
{
    public class BaseLogger : IBaseLogger
    {
		private IFileLogger _fileLogger;
		private ISeqLogger _seqLogger;
		private IMSqlServerLogger _sqlLogger;
		private IHttpContextAccessor _httpContextAccessor;
        public BaseLogger()
        {
            _fileLogger = ServiceTool.ServiceProvider.GetService<IFileLogger>();
            _seqLogger = ServiceTool.ServiceProvider.GetService<ISeqLogger>();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _sqlLogger = ServiceTool.ServiceProvider.GetService<IMSqlServerLogger>();
        }

        public string LogAddAspect(IInvocation invocation,Type _type)
        {
			var fullName = invocation.Method.ReflectedType.FullName; //Business.Abstract.IAuthService
			var ClassName = fullName.Substring(19, fullName.Length - 26);
			bool isConfirm = false;

			var path = Environment.CurrentDirectory;


			if (_type == typeof(FileLogger))
			{
				_fileLogger.Configure(ClassName);
				isConfirm = true;
			}

			if (_type == typeof(SeqLogger))
			{
				_seqLogger.Configure(ClassName);
				isConfirm = true;
			}

			if (_type == typeof(MSqlServerLogger))
			{
				_sqlLogger.Configure();
				isConfirm = true;
			}


			if (isConfirm == false)
			{
				throw new LogException();
			}

			var result = GetLogDetail(invocation);
			string json = JsonConvert.SerializeObject(result, Formatting.Indented);
			Console.WriteLine(json);
			return json;
		}

        public string LogAddMiddleware(HttpContext httpContext ,Type _type)
        {
			var path = httpContext.Request.Path.ToString();
			string[] splitpath = path.Split('/');
			
			string classname = splitpath[splitpath.Length - 2];
			if (classname == "api")
            {
				classname = splitpath[splitpath.Length - 1];
			}

			bool isConfirm = false;

			if (_type == typeof(FileLogger))
			{
				_fileLogger.Configure(classname);
				isConfirm = true;
			}

			if (_type == typeof(SeqLogger))
			{
				_seqLogger.Configure(classname);
				isConfirm = true;
			}

			if (_type == typeof(MSqlServerLogger))
			{
				_sqlLogger.Configure();
				isConfirm = true;
			}


			if (isConfirm == false)
			{
				throw new LogException();
			}

			var result = GetLogDetailMiddleware();
			string json = JsonConvert.SerializeObject(result, Formatting.Indented);
			Console.WriteLine(json);
			return json;
		}

        private LogDetail GetLogDetail(IInvocation invocation)
		{
			string path = _httpContextAccessor.HttpContext.Request.Path.Value;
			var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
			var methodName = _httpContextAccessor.HttpContext.Request.Method;

			var logDetail = new LogDetail
			{
				MethodName = invocation.Method.Name,
				IpAddress = remoteIpAddress,
				Path = path
			};

			return logDetail;
		}

		private LogDetail GetLogDetailMiddleware()
		{
			string path = _httpContextAccessor.HttpContext.Request.Path.Value;
			var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			var logDetail = new LogDetail
			{ 
				MethodName = "Middlware Logger",
				IpAddress = remoteIpAddress,
				Path = path
			};

			return logDetail;
		}
	}
}
