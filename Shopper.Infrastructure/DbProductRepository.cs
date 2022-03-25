using Microsoft.EntityFrameworkCore;
using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class DbProductRepository : DbEntityRepository<Product>, IProductRepository
    {
        public DbProductRepository(ShopperContext context) : base(context)
        {
        }

        public Task<Product> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAsync(ProductSearchCriteria searchCriteria)
        {
            var query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.Color))
            {
                query = query.Where(p => p.Color.Equals(searchCriteria.Color, StringComparison.OrdinalIgnoreCase));
            }

            if (searchCriteria.From.HasValue)
            {
                query = query.Where(p => p.Price >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(p => p.Price <= searchCriteria.To);
            }

            IEnumerable<Product> results = query.ToList();  // -> tutaj nastąpi przetworzenie zapytania (Expression)

            return Task.FromResult(results);
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
