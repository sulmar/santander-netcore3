using Microsoft.EntityFrameworkCore;
using Shopper.Domain;
using Shopper.Domain.Models;
using Shopper.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Infrastructure
{
    // dotnet add package Microsoft.EntityFrameworkCore
    public class ShopperContext : DbContext
    {
        public ShopperContext(DbContextOptions<ShopperContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            // modelBuilder.ApplyConfigurationsFromAssembly();

        }



    }
}
