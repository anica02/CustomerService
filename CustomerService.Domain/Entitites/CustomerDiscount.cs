using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entitites
{
    public class CustomerDiscount:Entity
    {
        public int CustomerId { get; set; }
        public int DiscountPercentage { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
