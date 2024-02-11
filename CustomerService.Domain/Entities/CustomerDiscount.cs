using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entities
{
    public class CustomerDiscount
    {
        public int CustomerId { get; set; }
        public int DiscountPercentage { get; set; }
        public int AgentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public virtual User Agent { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
