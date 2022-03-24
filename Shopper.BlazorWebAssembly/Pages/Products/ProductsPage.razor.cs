using Microsoft.AspNetCore.Components;
using Shopper.BlazorWebAssembly.IServices;
using Shopper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.BlazorWebAssembly.Pages.Products
{
    public partial class ProductsPage 
    {
        [Inject]
        public IProductService productRepository { get; set; }

        private IEnumerable<Product> products;

        protected override async Task OnInitializedAsync()
        {
            products = await productRepository.GetAsync();            

        }

    }

    
}
