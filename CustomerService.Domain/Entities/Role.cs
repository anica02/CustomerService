using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Entities
{
    public class Role:Entity
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public ICollection<RoleUseCase> RoleUseCases { get; set; } = new HashSet<RoleUseCase>();
    }
}
