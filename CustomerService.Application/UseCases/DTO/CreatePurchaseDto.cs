using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCases.DTO
{
    public class CreatePurchaseDto
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }

        public string Status { get; set; }
        public DateTime ActiveTill { get; set; }

        public bool UseDiscountIfGiven { get; set; }
    }
}




