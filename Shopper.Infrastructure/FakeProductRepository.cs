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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetByColorAsync(string color)
        {
            var results = products.Values.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(results);
        }
    }
}
