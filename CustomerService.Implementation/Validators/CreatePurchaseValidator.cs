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
    public class CreatePurchaseValidator:AbstractValidator<CreatePurchaseDto>
    {
        public CreatePurchaseValidator(CustomerServiceContext context)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Customer id is required")
                .Must(x => context.Customers.Any(p => p.Id == x && p.IsActive))
                .WithMessage("Customer id does not exist");

            RuleFor(x => x.ServiceId).NotEmpty()
                .WithMessage("Service id is required")
                .Must(x => context.Services.Any(p => p.Id == x && p.IsActive))
                .WithMessage("Service id does not exist");

            RuleFor(x => x.Status).NotEmpty()
               .WithMessage("Status is required");

            RuleFor(x => x.ActiveTill).NotEmpty()
               .WithMessage("ActiveTill is required").GreaterThanOrEqualTo(x => DateTime.Today);

            RuleFor(x => x.UseDiscountIfGiven).NotEmpty()
              .WithMessage("UseDiscountIfGiven is required");

        }
    }
 
}
