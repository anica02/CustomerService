using CustomerService.Application;
using CustomerService.Application.Exceptions;
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
    public class EfCreateCustomerDiscountCommand : EfUseCase, ICreateCustomerDiscountCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateCustomerDiscountValidator _validator;
        public EfCreateCustomerDiscountCommand(CustomerServiceContext context, 
            IApplicationActor actor
            , CreateCustomerDiscountValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }
        public int Id => 1;

        public string Name => "Add customer discount";

        public string Description => "Agent uses this use case to give a discount to loyal customer";

        public void Execute(CreateCustomerDiscountDto request)
        {
            _validator.ValidateAndThrow(request);
            var now = DateTime.Today;

            var customersDiscountAddedTodayByLoggedAgent = Context.CustomerDiscounts.Where(x => x.AgentId == _actor.Id && x.CreatedAt.Year == now.Year && x.CreatedAt.Month == now.Month && x.CreatedAt.Day == now.Day).ToList();
           
            if(customersDiscountAddedTodayByLoggedAgent.Count == 5)
            {
                throw new CustomerDiscountException(_actor.Id);
            }

            CustomerDiscount cd = new CustomerDiscount();
            cd.AgentId = _actor.Id;
            cd.CustomerId = request.CustomerId;
            cd.DiscountPercentage = request.DiscountPercentage;
            cd.IsActive = true;

            Context.CustomerDiscounts.Add(cd);
            Context.SaveChanges();
        }
    }
}
