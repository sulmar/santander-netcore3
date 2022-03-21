using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class FakeProductRepository : IProductRepository
    {
        private readonly IDictionary<int, Product> products;

        public FakeProductRepository()
        {
            products = new Dictionary<int, Product>
            {
                { 1, new Product { Id = 1, Name = "Product1", Color = "Black", Price = 0.99m } },
                { 2, new Product { Id = 2, Name = "Product2", Color = "Blue", Price = 10.99m } } ,
                { 3, new Product { Id = 3, Name = "Product3", Color = "Red", Price = 0.59m } },
            };
        }

        public Task AddAsync(Product product)
        {
            product.Id = products.Values.Max(p => p.Id) + 1;

            products.Add(product.Id, product);

            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return Task.FromResult(products.ContainsKey(id));
        }

        public Task<IEnumerable<Product>> GetAsync()
        {
            return Task.FromResult(products.Values.AsEnumerable());
        }

        public Task<Product> GetAsync(int id)
        {
            if (products.TryGetValue(id, out Product product))
            {
                return Task.FromResult(product);
            }
            else
            {
                return Task.FromResult<Product>(null);
            }
        }

        public Task<Product> GetAsync(string name)
        {
            var product = products.Values.SingleOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(product);
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

        public Task<IEnumerable<Product>> GetByColorAsync(string color)
        {
            var results = products.Values.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(results);
        }

        public Task RemoveAsync(int id)
        {
            products.Remove(id);

            return Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
        {
            await RemoveAsync(product.Id);

            products.Add(product.Id, product);            
        }
    }
}
