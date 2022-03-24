using Shopper.BlazorWebAssembly.IServices;
using Shopper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shopper.BlazorWebAssembly.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;

        public ProductService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var products = await client.GetFromJsonAsync<IEnumerable<Product>>("api/products");

            return products;
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await client.GetFromJsonAsync<Product>($"api/products/{id}");

            return product;
        }
    }
}
