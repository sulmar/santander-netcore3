using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // Logger (middleware)
            app.Use(async (context, next) =>
            {
                logger.LogInformation("{0} {1}", context.Request.Method, context.Request.Path);

                await next();

                logger.LogInformation("StatusCode: {0}", context.Response.StatusCode);

            });

            // Secret-Key (middleware)
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.TryGetValue("Secret-Key", out StringValues secretKey))
                {
                    if (secretKey == "123")
                    {
                        await next();
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }                    
                }
            });

            app.Run(context => context.Response.WriteAsync($"Hello {context.Request.Path}"));
        }
    }
}
