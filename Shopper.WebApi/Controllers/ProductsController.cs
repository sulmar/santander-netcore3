using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
using Shopper.Domain.Models;
using Shopper.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.WebApi.Controllers
{
    [Route("api/[controller]")] // Prefix
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(
            IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET api/products
        //[HttpGet]
        //public async Task<IEnumerable<Product>> Get()
        //{
        //    var products = await productRepository.GetAsync();

        //    return products;
        //}

        // GET api/products/{id}
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await productRepository.GetAsync(id);

            //if (product==null)
            //{                 
            //    return new NotFoundResult();          // 404 Not Found
            //}

            //return new OkObjectResult(product); // 200 OK

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET api/products/{weight}
        //[HttpGet("{weight:float}")]
        //public async Task<ActionResult<Product>> Get(float weight)
        //{
        //    return Ok();
        //}

        // GET api/products/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<Product>> Get(string name)
        {
            var product = await productRepository.GetAsync(name);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // query string

        // GET api/products?color=red
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetByColor(string color)
        //{
        //    IEnumerable<Product> products;

        //    if (string.IsNullOrEmpty(color))
        //    {
        //        products = await productRepository.GetAsync();
        //    }
        //    else
        //    {
        //        products = await productRepository.GetByColorAsync(color);
        //    }

        //    return Ok(products);
        //}

        // GET api/products?color=red&from=100&to=200
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(ProductSearchCriteria searchCriteria)
        {
            var products = await productRepository.GetAsync(searchCriteria);

            return Ok(products);
        }


        // POST api/products
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromServices] IMessageService messageService, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await productRepository.AddAsync(product);

            await messageService.SendAsync($"Dodano nowy produkt {product.Name}");

            // return Created($"http://localhost:5000/api/products/{product.Id}", product);

            // return new CreatedAtRouteResult("GetProductById", new { Id = product.Id }, product);

            return CreatedAtRoute("GetProductById", new { Id = product.Id }, product);
        }

        // PUT api/products/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await productRepository.UpdateAsync(product);

            return NoContent();
        }

        // DELETE api/products/{id:int}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await productRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            await productRepository.RemoveAsync(id);

            return Ok();
        }

        // HEAD api/products/{id:int}
        [HttpHead("{id}")]
        public async Task<ActionResult> Head(int id)
        {
            if (await productRepository.ExistsAsync(id))
                return Ok();
            else
                return NotFound();
        }

        // dotnet add package Microsoft.AspNetCore.JsonPatch
        // Content-Type: application/json-patch+json
        // Note: add to Startup: services.AddControllers().AddNewtonsoftJson()

        // PATCH api/products/{id:int}
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Product> patchProduct)
        {
            // System.Text.Json.Serialization

            Product product = await productRepository.GetAsync(id);

            patchProduct.ApplyTo(product);

            await productRepository.UpdateAsync(product);

            return Ok();                        
        }

        // https://datatracker.ietf.org/doc/html/rfc7386
        // Content-Type: application/merge-patch+json

        // GET api/customers/{customerId}/products
        [HttpGet("/api/customers/{customerId}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCustomer(int customerId)
        {
            var products = await productRepository.GetByCustomer(customerId);

            return Ok(products);
        }

    }
}
