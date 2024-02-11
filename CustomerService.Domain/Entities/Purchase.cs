using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entities
{
    public class Purchase:Entity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int ServiceId { get; set; }
        public  virtual Service Service { get; set; }

        public string Status { get; set; }

        public bool DiscountUsed { get; set; }

        public DateTime ActiveTill { get; set; }


    }
}
