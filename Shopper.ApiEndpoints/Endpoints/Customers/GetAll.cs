using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
using Shopper.Domain.Models;

namespace Shopper.ApiEndpoints.Endpoints.Customers
{

    // dotnet add package Ardalis.ApiEndpoints

    // GET api/customers
    public class GetAll : EndpointBaseAsync.WithoutRequest.WithResult<IEnumerable<Customer>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetAll(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("api/customers")]
        public override async Task<IEnumerable<Customer>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var customers = await customerRepository.GetAsync();

            return customers;
        }
    }


}
