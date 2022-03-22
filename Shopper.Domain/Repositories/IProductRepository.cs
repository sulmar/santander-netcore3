using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        Task<Product> GetAsync(string name);
        Task<IEnumerable<Product>> GetByColorAsync(string color);
        Task<IEnumerable<Product>> GetAsync(ProductSearchCriteria searchCriteria);
        Task<IEnumerable<Product>> GetByCustomer(int customerId);
    }
}
