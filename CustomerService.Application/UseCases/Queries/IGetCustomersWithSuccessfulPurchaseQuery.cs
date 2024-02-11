using CustomerService.Application.UseCases.DTO;
using CustomerService.Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCases.Queries
{
    public interface IGetCustomersWithSuccessfulPurchaseQuery:IQuery<CustomerSearch, IEnumerable<CustomerPurchasesDto>>
    {
    }
}
