using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.DataAccess.Configurations
{
    public class AddressConfiguration : EntityConfiguration<Address>
    {
        protected override void ConfigurationEntity(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Zip).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.Street).IsRequired();

          
        }
    }
}
