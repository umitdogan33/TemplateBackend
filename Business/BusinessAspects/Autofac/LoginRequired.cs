using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constans;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using Business.Abstract;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Core.Utilities.Messages;

namespace Business.BusinessAspects.Autofac
{
	public class LoginRequired:MethodInterception
	{
        private IHttpContextAccessor httpContext;

        public LoginRequired() {
            httpContext = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }



        protected override void OnBefore(IInvocation invocation)
        {
            var result = httpContext.HttpContext.User.Identity.IsAuthenticated;
            //string Token = httpContext.HttpContext.Request.Headers["token"];
            if (result == false)
            {
                throw new LoginRequiredException();
                return;
            }

        }
    }
}
