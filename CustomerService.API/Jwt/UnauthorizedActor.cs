using CustomerService.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.API.Jwt
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Email => "";

        public string Username => "unauthorized";

        public string RoleName => "none";

        public IEnumerable<int> AllowedUseCases => new List<int> { 6, 7, 18, 21, 22 };
    }
}
