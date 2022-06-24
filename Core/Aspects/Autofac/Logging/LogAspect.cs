using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.logging.Serilog;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.IO;
namespace Core.Aspects.Autofac.Logging
{
	public class LogAspect : MethodInterception
	{
		private IHttpContextAccessor _httpContextAccessor;
		private Type _type;
		private IBaseLogger _baseLogger;

		public LogAspect(Type type)
		{
			_httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
			_baseLogger = ServiceTool.ServiceProvider.GetService<IBaseLogger>();
			_type = type;
		}

		protected override void OnBefore(IInvocation invocation)
		{
			var fullName = invocation.Method.ReflectedType.FullName; 
				
			var json = _baseLogger.LogAddAspect(invocation, _type);
			Log.Information(json);
		}
	}
}
