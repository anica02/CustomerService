using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entities
{
    public class Service:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Price Price { get; set; }
        public string TypeOfService { get; set; }

        public ICollection<Purchase> Orders { get; set; } = new HashSet<Purchase>();
    }
}
