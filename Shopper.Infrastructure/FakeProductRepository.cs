using Bogus;
using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{

    public class FakeProductRepository : FakeEntityRepository<Product>, IProductRepository
    {
        private IDictionary<int, Product> products => entities;

        public FakeProductRepository(Faker<Product> faker)
            : base(faker)
        {
        }

        
        public Task<IEnumerable<Product>> GetAsync(ProductSearchCriteria searchCriteria)
        {
            var query = products.Values.AsQueryable();

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

        public Task<Product> GetAsync(string name)
        {
            var product = entities.Values.SingleOrDefault(e => e.Name == name);

            return Task.FromResult(product);
        }

        public Task<IEnumerable<Product>> GetByColorAsync(string color)
        {
            var results = products.Values.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(results);
        }

        public Task<IEnumerable<Product>> GetByCustomer(int customerId)
        {
            var results = products.Values.AsEnumerable();

            return Task.FromResult(results);
        }
    }
}
