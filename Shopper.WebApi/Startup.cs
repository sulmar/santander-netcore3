using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Shopper.Domain;
using Shopper.Domain.Models;
using Shopper.Domain.Services;
using Shopper.Domain.Validators;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;
using Shopper.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shopper.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services
                 .AddControllers()
                 .AddFluentValidation()
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //});
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));

            // services.AddFakeServices();

            services.AddDbServices();

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContext<DbContext, ShopperContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("ShopperConnection")));

            string address = Configuration["EmailMessageService:Address"];
            int port = int.Parse(Configuration["EmailMessageService:Port"]);
            string googleSecretKey = Configuration["GoogleMapSecretKey"];

            services.AddSingleton<IMessageService, FakeEmailMessageService>();

            services.Configure<FakeEmailMessageServiceOptions>(Configuration.GetSection("EmailMessageService"));

            // dotnet add package NSwag.AspNetCore
            services.AddOpenApiDocument();

            services.AddTransient<IValidator<Customer>, CustomerValidator>();
            services.AddTransient<IValidator<Product>, ProductValidator>();

            services.AddHttpContextAccessor();

            services.AddCors(policy =>
            {
                policy.AddDefaultPolicy(
                    options => 
                    options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();            

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/dashboard", context => context.Response.WriteAsync("Hello Dashboard!"));
            });
        }
    }
}
