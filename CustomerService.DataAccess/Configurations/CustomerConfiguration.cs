using CustomerService.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.DataAccess.Configurations
{
    public class CustomerConfiguration : EntityConfiguration<Customer>
    {
        protected override void ConfigurationEntity(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
        {
           
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SSN).IsRequired();
            builder.HasMany(x => x.Orders).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasOne(x => x.UserDiscount).WithOne(x => x.Customer).HasForeignKey<CustomerDiscount>(x => x.CustomerId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
