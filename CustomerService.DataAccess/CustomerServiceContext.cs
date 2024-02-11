
using CustomerService.DataAccess.DTO;
using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerService.DataAccess
{
    public class CustomerServiceContext:DbContext
    {
        public CustomerServiceContext()
        {

        }

        public CustomerServiceContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS; Initial Catalog = CustomerService; Integrated Security = true");
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerServiceContext).Assembly);
           
       
            modelBuilder.Entity<RoleUseCase>().HasKey(x => new { x.RoleId, x.UseCaseId });
            modelBuilder.Entity<Price>().Property(x => x.ServicePrice).IsRequired();
            modelBuilder.Entity<CustomerDiscount>().Property(x => x.DiscountPercentage).IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.Status).IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.DiscountUsed).IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.ActiveTill).IsRequired();

            modelBuilder.Entity<Customer>()
            .HasOne(u => u.Home)
            .WithMany()
            .HasForeignKey(u => u.HomeId)
            .IsRequired(false);

            modelBuilder.Entity<Customer>()
                .HasOne(u => u.Office)
                .WithMany()
                .HasForeignKey(u => u.OfficeId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    }

    public class MyDataProcessor
    {
        private Customer MapEmployeeDtoToUser(EmployeeDTO eDTO)
        {
            var customer = new Customer
            {
                Name = eDTO.Name,
                SSN = eDTO.SSN,
                DOB = eDTO.DOB.HasValue ? eDTO.DOB.Value : null,

                Age = eDTO.Age,
                FavoriteColors = eDTO.FavoriteColors != null ? string.Join(",", eDTO.FavoriteColors) : "",
                Title = eDTO.Title,
                Salary = eDTO.Salary,
                Notes = eDTO.Notes,
                Picture = eDTO.Picture


            };

            if (eDTO.Spouse != null)
            {
                customer.Spouse = new Customer
                {
                    Name = eDTO.Spouse.Name,
                    SSN = eDTO.Spouse.SSN,
                    DOB = eDTO.Spouse.DOB.HasValue ? eDTO.Spouse.DOB.Value : null,
                    Age = eDTO.Spouse.Age,
                    FavoriteColors = eDTO.Spouse.FavoriteColors != null ? string.Join(",", eDTO.Spouse.FavoriteColors) : ""
                };
            }

            return customer;
        }
        private Customer MapPersonDtoToUser(PersonDTO pDTO)
        {
            var user = new Customer
            {
                Name = pDTO.Name,
                SSN = pDTO.SSN,
                DOB = pDTO.DOB.HasValue ? pDTO.DOB.Value : null,
                Age = pDTO.Age.Value,
                FavoriteColors = pDTO.FavoriteColors != null ? string.Join(",", pDTO.FavoriteColors) : ""

            };

            if (pDTO.Spouse != null)
            {
                user.Spouse = new Customer
                {
                    Name = pDTO.Spouse.Name,
                    SSN = pDTO.Spouse.SSN,
                    DOB = pDTO.Spouse.DOB.HasValue ? pDTO.Spouse.DOB.Value : null,
                    Age = pDTO.Spouse.Age,
                    FavoriteColors = pDTO.Spouse.FavoriteColors != null ? string.Join(",", pDTO.Spouse.FavoriteColors) : ""
                };
            }

            return user;
        }
        private Domain.Entities.Address MapAddressDtoToEntity(AddressDTO aDTO)
        {
            return new Domain.Entities.Address
            {
                Zip = aDTO.Zip,
                City = aDTO.City,
                State = aDTO.State,
                Street = aDTO.Street
            };
        }
        public void SaveDataToDatabase(IEnumerable<object> data)
        {
            using (var dbContext = new CustomerServiceContext())
            {
                
              
                foreach (var p in data)
                {

                    if (p!=null && p.GetType() == typeof(PersonDTO))
                    {
                        var pDTO = (PersonDTO)p;
                        var user = MapPersonDtoToUser(pDTO);
                        var homeAddress = MapAddressDtoToEntity(pDTO.Home);
                        var officeAddress = MapAddressDtoToEntity(pDTO.Office);

                        dbContext.Addresses.Add(homeAddress);
                        dbContext.Addresses.Add(officeAddress);
                        dbContext.SaveChanges();

                        user.Home = homeAddress;
                        user.Office= officeAddress;

                        dbContext.Customers.Add(user);
                        dbContext.SaveChanges();
                    }
                    else if ( p!=null && p.GetType() == typeof(EmployeeDTO))
                    {
                        var eDTO = (EmployeeDTO)p;

                        var employee = MapEmployeeDtoToUser(eDTO);
                       
                        var homeAddress = MapAddressDtoToEntity(eDTO.Home);
                        
                        var officeAddress = MapAddressDtoToEntity(eDTO.Office);

                        dbContext.Addresses.Add(homeAddress);
                   
                        dbContext.Addresses.Add(officeAddress);
                        dbContext.SaveChanges();

                        employee.HomeId = homeAddress.Id;
                        employee.OfficeId = officeAddress.Id;

                        if(employee.Spouse != null)
                        {
                            var homeAddress2 = MapAddressDtoToEntity(eDTO.Spouse.Home);
                            var officeAddress2 = MapAddressDtoToEntity(eDTO.Spouse.Office);

                            dbContext.Addresses.Add(homeAddress2);
                            dbContext.Addresses.Add(officeAddress2);
                            dbContext.SaveChanges();
                            employee.Spouse.HomeId = homeAddress2.Id;
                            employee.Spouse.OfficeId = officeAddress2.Id;

                        }
                        dbContext.Customers.Add(employee);
                        dbContext.SaveChanges();
                        
                    }

                }
                dbContext.SaveChanges();
            }
        }
    }

   

   
}
