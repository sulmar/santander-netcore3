using Microsoft.EntityFrameworkCore;
using Shopper.Domain;
using Shopper.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{

    public class DbCustomerRepository : DbEntityRepository<Customer>, ICustomerRepository
    {
        public DbCustomerRepository(ShopperContext context) : base(context)
        {
        }

        public Task<bool> ExistsAsync(string email)
        {
            return entities.AnyAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Customer>> Get(Gender gender)
        {
            var list = await entities.Where(e => e.Gender == gender).ToListAsync();

            return list;
        }
    }
}
