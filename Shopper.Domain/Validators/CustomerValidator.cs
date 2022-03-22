using FluentValidation;
using Shopper.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Shopper.Domain.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerValidator(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public CustomerValidator()
        {
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword);
            RuleFor(p => p.Email).MustAsync(ExistsEmail);

        }

        private Task<bool> ExistsEmail(string email, CancellationToken cancellationToken)
        {
            return customerRepository.ExistsAsync(email);
        }
    }
}
