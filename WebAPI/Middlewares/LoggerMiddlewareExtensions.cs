using Microsoft.AspNetCore.Builder;

namespace WebAPI.Middlewares
{
    public static class AuthorizationMiddlewareExtensions2
    {
        public static void CentrolLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggerMiddleware>();
        }
    }
}
