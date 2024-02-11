using CustomerService.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.UseCases.Commands
{
    public interface ICreatePurchaseCommand:ICommand<CreatePurchaseDto>
    {

    }
}
