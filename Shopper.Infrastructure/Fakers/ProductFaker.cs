using Bogus;
using Shopper.Domain;

namespace Shopper.Infrastructure.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()));

        }
    }
}
