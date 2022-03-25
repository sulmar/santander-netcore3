using Microsoft.Extensions.DependencyInjection;
using Shopper.Domain;
using Shopper.Infrastructure;

namespace Shopper.WebApi.Extensions
{
    public static class DbServiceExtensions
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {

            services.AddScoped<IProductRepository, DbProductRepository>();
            services.AddScoped<ICustomerRepository, DbCustomerRepository>();

            return services;
        }
    }
}
