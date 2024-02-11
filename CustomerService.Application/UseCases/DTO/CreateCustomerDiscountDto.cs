using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCases.DTO
{
    public class CreateCustomerDiscountDto
    {
        public int CustomerId { get; set; }
        public int DiscountPercentage { get; set; }
    }
}
