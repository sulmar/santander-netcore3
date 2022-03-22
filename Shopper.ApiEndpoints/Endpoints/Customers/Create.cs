using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shopper.ApiEndpoints.Endpoints.Customers
{
    public class Create : EndpointBaseAsync.WithRequest<Customer>.WithActionResult<Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public Create(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpPost("api/customers")]
        public override async Task<ActionResult<Customer>> HandleAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await customerRepository.AddAsync(customer);

            return CreatedAtRoute(nameof(GetById), new { Id = customer.Id }, customer);
        }
    }
}
