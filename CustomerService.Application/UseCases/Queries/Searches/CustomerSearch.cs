using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCases.Queries.Searches
{
    public class CustomerSearch
    {
        public int? CustomerId { get; set; }
        public  int? ServiceId { get; set; }

    }
}
