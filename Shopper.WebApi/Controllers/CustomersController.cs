using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Prefix
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET api/customers
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            var customers = await customerRepository.GetAsync();

            return customers;
        }


        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}", Name = "GetCustomerById")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var customer = await customerRepository.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            await customerRepository.AddAsync(customer);

            return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        }

        // PUT api/customers/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            await customerRepository.UpdateAsync(customer);

            return NoContent();
        }

        // DELETE api/customers/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await customerRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            await customerRepository.RemoveAsync(id);

            return Ok();
        }

        // HEAD api/customers/{id:int}
        [HttpHead("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Head(int id)
        {
            if (await customerRepository.ExistsAsync(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
