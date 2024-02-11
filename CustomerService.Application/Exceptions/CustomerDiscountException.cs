using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Exceptions
{
    public class CustomerDiscountException:Exception
    {
        public CustomerDiscountException(int id): base($"Agent of id {id} has a limit of 5 customers per day")
        {

        }
    }
}
