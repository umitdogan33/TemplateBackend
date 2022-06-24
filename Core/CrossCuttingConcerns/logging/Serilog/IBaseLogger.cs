using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.logging.Serilog
{
    public interface IBaseLogger
    {
        string LogAddAspect(IInvocation invocation, Type _type);
        public string LogAddMiddleware(HttpContext httpContext,Type _type);
    }
}
