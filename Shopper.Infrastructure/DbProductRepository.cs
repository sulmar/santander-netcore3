using Microsoft.EntityFrameworkCore;
using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class DbProductRepository : DbEntityRepository<Product>, IProductRepository
    {
        public DbProductRepository(DbContext context) : base(context)
        {
        }

        public Task<Product> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAsync(ProductSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetByColorAsync(string color)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
