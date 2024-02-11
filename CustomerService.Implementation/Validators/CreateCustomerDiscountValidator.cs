using CustomerService.Application.UseCases.DTO;
using CustomerService.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Implementation.Validators
{
    public class CreateCustomerDiscountValidator : AbstractValidator<CreateCustomerDiscountDto>
    {
        public CreateCustomerDiscountValidator(CustomerServiceContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Customer id is required")
                .Must(x => context.Customers.Any(p => p.Id == x && p.IsActive))
                .WithMessage("Customer id does not exist");

            RuleFor(x => x.DiscountPercentage).NotEmpty()
                .WithMessage("Discount percentage is required")
                .GreaterThan(0).WithMessage("Discount percetnage must be greater that 0");
        }
    }
}
