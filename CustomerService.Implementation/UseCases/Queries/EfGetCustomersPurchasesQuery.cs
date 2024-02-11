using CustomerService.Application.UseCases.DTO;
using CustomerService.Application.UseCases.Queries;
using CustomerService.Application.UseCases.Queries.Searches;
using CustomerService.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Implementation.UseCases.Queries
{
    public class EfGetCustomersPurchasesQuery : EfUseCase, IGetCustomersWithSuccessfulPurchaseQuery
    {
        public EfGetCustomersPurchasesQuery(CustomerServiceContext context) : base(context)
        {

        }

        public int Id => 3;

        public string Name => "Get customers with purchases";

        public string Description => "List of customers with purchases with used discounts";

        public IEnumerable<CustomerPurchasesDto> Execute(CustomerSearch search)
        {
            var query = Context.Customers.Where(x => x.Orders.Any(x => x.DiscountUsed)).AsQueryable();

            if (search.CustomerId.HasValue)
            {
                query = query.Where(x => x.Orders.Any(x => x.CustomerId == search.CustomerId));
            }


            if (search.ServiceId.HasValue)
            {
                query = query.Where(x => x.Orders.Any(x => x.ServiceId == search.ServiceId));
            }

            IEnumerable<CustomerPurchasesDto> result = query.Select(x => new CustomerPurchasesDto
            {
                Name = x.Name,
                Age = x.Age,
                SSN = x.SSN,
                DOB = x.DOB,
                Home = new AddressDto
                {
                    Zip = x.Home.Zip,
                    City = x.Home.City,
                    State = x.Home.State,
                    Street = x.Home.Street
                },
                Office = new AddressDto
                {
                    Zip = x.Office.Zip,
                    City = x.Office.City,
                    State = x.Office.State,
                    Street = x.Office.Street
                },
                FavoriteColors = x.FavoriteColors,
                Title = x.Title,
                Salary = x.Salary,
                Notes = x.Notes,
                Picture = x.Picture,
                Purchases = x.Orders.Select(p => new PurchaseDto
                {
                    ServiceId = p.ServiceId,
                    ServiceName = p.Service.Name,
                    Status = p.Status,
                    DiscountUsed = p.DiscountUsed,
                    ActiveTill = p.ActiveTill,
                    Price = p.Service.Price.ServicePrice
                }).ToList(),
                DiscountPercentage = x.UserDiscount.DiscountPercentage

            }).ToList();

            return result;
        }
    }
 
}
