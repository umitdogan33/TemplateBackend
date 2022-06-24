using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.Helpers.RefreshTokenHelper;
using Castle.DynamicProxy;
using Core.Utilities.Helpers.Email;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
   public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();

            
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<EmailHelper>().As<IEmailHelper>().SingleInstance();
            builder.RegisterType<RefreshTokenHelper>().As<IRefreshTokenHelper>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            builder.RegisterType<RefreshTokenManager>().As<IRefreshTokenService>().SingleInstance();
            builder.RegisterType<EfRefreshTokenDal>().As<IRefreshTokenDal>().SingleInstance();
            builder.RegisterType<DenemeManager>().As<IDenemeService>().SingleInstance();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                    .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                    {
                        Selector = new AspectInterceptorSelector()
                    }).SingleInstance();
        
    }
    }
}
