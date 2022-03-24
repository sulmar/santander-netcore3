using Shopper.BlazorWebAssembly.IServices;
using Shopper.Domain;
using System.Collections.Generic;
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

        public async Task AddAsync<Product>(Product product)
        {
            await client.PostAsJsonAsync("api/products", product);
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

        public async Task RemoveAsync(int id)
        {
            await client.DeleteAsync($"api/products/{id}");
        }

        public async Task UpdateAsync<Product>(int id, Product product)
        {
            await client.PutAsJsonAsync($"api/products/{id}", product);
        }
    }
}
