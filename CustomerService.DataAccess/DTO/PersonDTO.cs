using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.DataAccess.DTO
{
    public class PersonDTO
    {

        public string Name { get; set; }

        public string SSN { get; set; }

        public System.DateTime? DOB { get; set; }

        public AddressDTO Home { get; set; }

        public AddressDTO Office { get; set; }

        public PersonDTO? Spouse { get; set; }

        public string[]? FavoriteColors { get; set; }

        public long? Age { get; set; }

    }

    public class AddressDTO
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}
