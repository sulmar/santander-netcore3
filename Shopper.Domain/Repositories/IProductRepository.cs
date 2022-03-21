using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetAsync(int id);
        Task<Product> GetAsync(string name);
        Task<IEnumerable<Product>> GetByColorAsync(string color);
        Task<IEnumerable<Product>> GetAsync(ProductSearchCriteria searchCriteria);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(int id);
        Task<bool> ExistsAsync(int id);


    }
}
