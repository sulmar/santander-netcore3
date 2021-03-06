using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shopper.WebApi.Controllers
{
    [Authorize]
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
        // [Authorize(Roles = "developer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            //if (!this.User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}

            if (!this.User.IsInRole("developer"))
            {
                return Forbid();                
            }

            var categories = this.User.FindAll("kat").Select(c => c.Value);

            var email = this.User.FindFirstValue(ClaimTypes.Email);

            var customers = await customerRepository.GetAsync();


            return Ok(customers);
        }


      //  [Authorize(AuthenticationSchemes = "Abc")]
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

        // POST api/customers
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Customer>> PostFromBody([FromBody] Customer customer)
        {
            if (await customerRepository.ExistsAsync(customer.Email))
            {
                ModelState.AddModelError(nameof(Customer.Email), $"Adres email {customer.Email} już istnieje");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await customerRepository.AddAsync(customer);

            return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        }


        // POST api/customers
        //[HttpPost]
        //[Consumes("application/x-www-form-urlencoded")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<Customer>> PostFromForm([FromForm] Customer customer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    await customerRepository.AddAsync(customer);

        //    return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        //}

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
