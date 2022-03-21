using Microsoft.AspNetCore.Mvc;
using Shopper.Domain;
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

        public ProductsController(IProductRepository productRepository)
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
        [HttpGet("{id:int}")]
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetByColor(string color)
        {
            IEnumerable<Product> products;

            if (string.IsNullOrEmpty(color))
            {
                products = await productRepository.GetAsync();
            }
            else
            {
                products = await productRepository.GetByColorAsync(color);
            }

            return Ok(products);

        }

    }
}
