using Microsoft.AspNetCore.Builder;

namespace WebAPI.Mddlewares
{
    public static class AuthorizationMiddlewareExtensions
    {
        public static void ConfigreAuthorization(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
