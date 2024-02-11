using CustomerService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected CustomerServiceContext Context { get; }

        protected EfUseCase(CustomerServiceContext context)
        {
            Context = context;
        }
    }
}
