using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Shopper.Api.Middlewares;
using Shopper.Domain;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Shopper.Api.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Shopper.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Faker<Product>, ProductFaker>();
            services.AddSingleton<IProductRepository, FakeProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            #region Use i Run

            // Logger (middleware)
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("{0} {1}", context.Request.Method, context.Request.Path);

            //    await next();

            //    logger.LogInformation("StatusCode: {0}", context.Response.StatusCode);

            //});

            // Secret-Key (middleware)
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.TryGetValue("Secret-Key", out StringValues secretKey))
            //    {
            //        if (secretKey == "123")
            //        {
            //            await next();
            //        }
            //        else
            //        {
            //            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        }                    
            //    }
            //});

            #endregion

            // app.UseMiddleware<LoggerMiddleware>();
            // app.UseMiddleware<SecretKeyMiddleware>();

            app.UseLogger();
           // app.UseSecretKey();

            app.UseRouting();

            // Route-To-Code

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    async context => await context.Response.WriteAsync("Hello World!"));

                // GET api/products
                endpoints.MapGet("api/products",
                    async context =>
                    {
                        IProductRepository productRepository = context.RequestServices.GetService<IProductRepository>();

                        if (productRepository == null)
                            throw new Exception("Please add service for IProductRepository");

                        var products = await productRepository.GetAsync();

                        await context.Response.WriteAsJsonAsync(products);
                    })
                .RequireAuthorization();


                // GET api/products/{id}

                endpoints.MapGet("api/products/{id:int}", async context =>
                {
                    int id = Convert.ToInt32(context.GetRouteValue("id"));

                    IProductRepository productRepository = context.RequestServices.GetRequiredService<IProductRepository>();

                    var product = await productRepository.GetAsync(id);

                    await context.Response.WriteAsJsonAsync(product);

                });


                // POST api/products

                endpoints.MapPost("api/products", async context =>
                {
                    var form = await context.Request.ReadFormAsync();

                    context.Response.StatusCode = StatusCodes.Status201Created;

                });


            });





            // GET api/customers

            // POST api/customers


            // app.Run(context => context.Response.WriteAsync($"Hello {context.Request.Path}"));


        }
    }
}
