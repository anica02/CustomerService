using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Email { get; }
        string Username { get; }
        string RoleName { get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
