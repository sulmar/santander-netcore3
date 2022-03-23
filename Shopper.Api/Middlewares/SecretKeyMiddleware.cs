using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Shopper.Api.Middlewares
{

    public static class SecretKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecretKey(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecretKeyMiddleware>();

            return app;
        }
    }

    public class SecretKeyMiddleware : AbstractMiddleware
    {
        public SecretKeyMiddleware(RequestDelegate next) : base(next)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Secret-Key", out StringValues secretKey))
            {
                if (secretKey == "123")
                {
                    await next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
        }
    }
}
