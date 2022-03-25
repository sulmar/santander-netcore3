using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shopper.Domain;
using Shopper.Domain.Models;
using Shopper.Domain.Services;
using Shopper.Domain.Validators;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;
using Shopper.WebApi.AuthenticationHandlers;
using Shopper.WebApi.Controllers;
using Shopper.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            services.AddDbContext<ShopperContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("ShopperConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ShopperConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

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

            //services.AddAuthentication(defaultScheme: "SecretKey")
            //    .AddScheme<AuthenticationSchemeOptions, SecretKeyAuthenticationHandler>("SecretKey", null)
            //    .AddScheme<AuthenticationSchemeOptions, SecretKeyAuthenticationHandler>("Abc", null)


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    string secretKey = "your-256-bit-secret";

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidAudience = "santander",
                        ValidateAudience = true,
                        ValidIssuer = "santander",
                        ValidateIssuer = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ValidateIssuerSigningKey = true
                    };
                });

            // dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

            services.AddTransient<IAuthorizeRepository, FakeAuthorizeRepository>();

            services.AddTransient<ITokenService, JwtTokenService>();

            services.AddTransient<UsersSeeder>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopperContext context, ApplicationDbContext applicationContext, UsersSeeder usersSeeder)
        {
            context.Database.Migrate();
            applicationContext.Database.Migrate();

            usersSeeder.AddUsers().Wait();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();            

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/dashboard", context => context.Response.WriteAsync("Hello Dashboard!"));
            });
        }


        public class UsersSeeder
        {
            private readonly UserManager<ApplicationUser> userManager;
            private readonly IPasswordHasher<ApplicationUser> passwordHasher;

            public UsersSeeder(IPasswordHasher<ApplicationUser> passwordHasher, UserManager<ApplicationUser> userManager)
            {
                this.passwordHasher = passwordHasher;
                this.userManager = userManager;
            }

            public async Task AddUsers()
            {
                var user1 = new ApplicationUser { UserName = "john", Email = "john.smith@domain.pl ", Account = "40000100" };

                user1.PasswordHash = passwordHasher.HashPassword(user1, "123");

                await AddUser(user1);


                var user2 = new ApplicationUser { UserName = "ann", Email = "ann.smith@domain.pl ", Account = "50000100" };

                user2.PasswordHash = passwordHasher.HashPassword(user1, "abc");

                await AddUser(user2);

            }

            private async Task<IdentityResult> AddUser(ApplicationUser applicationUser)
            {
                
                var user = await userManager.FindByNameAsync(applicationUser.UserName);

                if (user==null)
                {
                    var result = await userManager.CreateAsync(applicationUser);

                    return result;
                }

                else
                {
                    return IdentityResult.Success;
                }
            }
        }
    }
}
