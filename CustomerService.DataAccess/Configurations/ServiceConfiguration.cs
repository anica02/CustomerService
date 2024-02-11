using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.DataAccess.Configurations
{
    public class ServiceConfiguration : EntityConfiguration<Service>
    {
        protected override void ConfigurationEntity(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.TypeOfService).IsRequired();
            builder.HasOne(x => x.Price).WithOne(x => x.Service).HasForeignKey<Price>(x => x.ServiceId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasMany(x => x.Orders).WithOne(x => x.Service).HasForeignKey(x => x.ServiceId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            
        }
    }
}
