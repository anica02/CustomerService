using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entitites
{
    public class Price:Entity
    {
        public double ServicePrice { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
