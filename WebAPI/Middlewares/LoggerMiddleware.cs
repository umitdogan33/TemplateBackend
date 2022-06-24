using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.logging.Serilog;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace WebAPI.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private IBaseLogger _baseLogger;
        // TİPS = readonly amaç sadece değer atama

        public LoggerMiddleware(
            RequestDelegate next)
        {
            _next = next;
            _baseLogger = ServiceTool.ServiceProvider.GetService<IBaseLogger>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.ToString();
            string[] splitpath = path.Split('/');
            string classname = splitpath[splitpath.Length-2];
            var returnedValue = _baseLogger.LogAddMiddleware(httpContext,typeof(FileLogger));
            //_baseLogger.LogAddAspect(invocation,typeof(FileLogger));
            Log.Information(returnedValue);
            await _next(httpContext);

        }
    }
}
