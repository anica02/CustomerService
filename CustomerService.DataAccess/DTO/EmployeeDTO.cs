using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.DataAccess.DTO
{
    public  class EmployeeDTO:PersonDTO
    {

        public string Title { get; set; }

        public long Salary { get; set; }


        public string Notes { get; set; }

        public byte[] Picture { get; set; }
    }


}
