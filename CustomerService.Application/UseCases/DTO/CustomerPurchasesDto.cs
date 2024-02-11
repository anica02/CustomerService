using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCases.DTO
{
    public class CustomerPurchasesDto
    {
        public string Name { get; set; }

        public string SSN { get; set; }

        public System.DateTime? DOB { get; set; }

        public AddressDto Home { get; set; }

        public AddressDto Office { get; set; }

        public string? FavoriteColors { get; set; }

        public long? Age { get; set; }

        public string? Title { get; set; }

        public long? Salary { get; set; }

        public string? Notes { get; set; }

        public byte[]? Picture { get; set; }

        public IEnumerable<PurchaseDto> Purchases { get; set; }

        public double DiscountPercentage { get; set; }
    }
    public class AddressDto
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
    public class PurchaseDto { 
    
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; }
        public bool DiscountUsed { get; set; }

        public DateTime ActiveTill { get; set; }

        public double Price { get; set; }

    }
}
