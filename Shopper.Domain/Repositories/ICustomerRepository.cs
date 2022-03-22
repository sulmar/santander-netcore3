using Shopper.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopper.Domain
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Task<IEnumerable<Customer>> Get(Gender gender);
    }
}
