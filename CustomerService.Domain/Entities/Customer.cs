using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entities
{
    public  class Customer:Entity
    {
        public string Name { get; set; }

        public string SSN { get; set; }

        public System.DateTime? DOB { get; set; }

        public int HomeId { get; set; }

        public int OfficeId { get; set; }

        public Customer? Spouse { get; set; }

        public string? FavoriteColors { get; set; }

        public long? Age { get; set; }
        public virtual Address Home { get; set; }
        public virtual Address Office { get; set; }

        public string? Title { get; set; }

        public long? Salary { get; set; }

        public string? Notes { get; set; }

        public byte[]? Picture { get; set; }

        public ICollection<Purchase> Orders { get; set; } = new HashSet<Purchase>();
        public CustomerDiscount UserDiscount { get; set; }
    }
}
