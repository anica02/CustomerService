﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entities
{
    public class User: Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public ICollection<CustomerDiscount> CustomerDiscounts  { get; set; } = new HashSet<CustomerDiscount>();
    }

}
