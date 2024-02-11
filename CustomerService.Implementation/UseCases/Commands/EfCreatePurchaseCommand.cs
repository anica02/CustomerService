using CustomerService.Application.UseCases.Commands;
using CustomerService.Application.UseCases.DTO;
using CustomerService.DataAccess;
using CustomerService.Domain.Entities;
using CustomerService.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Implementation.UseCases.Commands
{
    public class EfCreatePurchaseCommand:EfUseCase, ICreatePurchaseCommand
    {
        private readonly CreatePurchaseValidator _validator;
        public EfCreatePurchaseCommand(CustomerServiceContext context, CreatePurchaseValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Create purchase";

        public string Description => "At the request of the user, the agent enters the purchase into the database";

        public void Execute(CreatePurchaseDto request)
        {
            _validator.ValidateAndThrow(request);

            var customerHasDiscount = Context.CustomerDiscounts.Where(x => x.CustomerId == request.CustomerId && x.IsActive).FirstOrDefault();

            if(request.UseDiscountIfGiven && customerHasDiscount != null)
            {
                Purchase p = new Purchase();
                p.CustomerId = request.CustomerId;
                p.ServiceId = request.ServiceId;
                p.Status = request.Status;
                p.ActiveTill = request.ActiveTill;
                p.DiscountUsed = true;
                Context.Purchases.Add(p);
                Context.SaveChanges();

                customerHasDiscount.IsActive = false;

                Context.Entry(customerHasDiscount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                Purchase p = new Purchase();
                p.CustomerId = request.CustomerId;
                p.ServiceId = request.ServiceId;
                p.Status = request.Status;
                p.ActiveTill = request.ActiveTill;
                p.DiscountUsed = false;
                Context.Purchases.Add(p);
                Context.SaveChanges();
            }

        }
    }
}
