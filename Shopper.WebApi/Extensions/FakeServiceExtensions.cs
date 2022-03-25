using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Shopper.Domain;
using Shopper.Domain.Models;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.WebApi.Extensions
{

    public static class FakeServiceExtensions
    {
        public static IServiceCollection AddFakeServices(this IServiceCollection services)
        {
            AddFakeProductServices(services);
            AddFakeCustomerServices(services);

            return services;
        }

        private static void AddFakeCustomerServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
        }

        private static void AddFakeProductServices(IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, FakeProductRepository>();
            services.AddSingleton<Faker<Product>, ProductFaker>();
        }
    }
}
