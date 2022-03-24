using Shopper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.BlazorWebAssembly.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetAsync(int id);
        Task AddAsync<Product>(Product product);
        Task UpdateAsync<Product>(int id, Product product);
        Task RemoveAsync(int id);
    }
}
