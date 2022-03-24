using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Domain.Validators
{
    // dotnet add package FluentValidation

    
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).Length(3, 50).NotEmpty().WithName("Nazwa");
            RuleFor(p => p.Price).GreaterThan(0).NotEmpty().WithName("Cena");
            RuleFor(p => p.Color).NotEmpty().WithName("Kolor");

        }
    }

    
}
