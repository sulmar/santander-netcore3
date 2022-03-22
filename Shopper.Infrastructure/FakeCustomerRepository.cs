using Bogus;
using Shopper.Domain;
using Shopper.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class FakeCustomerRepository : FakeEntityRepository<Customer>, ICustomerRepository
    {
        public FakeCustomerRepository(Faker<Customer> faker) : base(faker)
        {
        }

        public Task<bool> ExistsAsync(string email)
        {
            var exists = entities.Any(e => e.Value.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(exists);
        }

        public Task<IEnumerable<Customer>> Get(Gender gender)
        {
            var customers = entities
                .Where(e => e.Value.Gender == gender)
                .Select(e=> e.Value);

            return Task.FromResult(customers);
        }

        public override async Task RemoveAsync(int id)
        {
            var customer = await GetAsync(id);

            customer.IsRemoved = true;
        }
    }
}
