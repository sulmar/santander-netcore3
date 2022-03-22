using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
using Shopper.Domain.Models;

namespace Shopper.ApiEndpoints.Endpoints.Customers
{
    public class GetById : EndpointBaseAsync.WithRequest<int>.WithActionResult<Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public GetById(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("api/customers/{id:int}", Name = nameof(GetById))]
        public override async Task<ActionResult<Customer>> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
            var customer = await customerRepository.GetAsync(request);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
    }


}
