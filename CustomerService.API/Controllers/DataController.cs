
using CustomerService.DataAccess;
using CustomerService.DataAccess.DTO;
using CustomerService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public readonly MyDataProcessor _dataProcessor;

        public CustomerServiceContext _context;

        private List<object> _allCustomers;

        public DataController(MyDataProcessor dataProcessor, CustomerServiceContext context)
        {
            _dataProcessor = new MyDataProcessor();
            _context = context;
           
        }
       

        [HttpGet]
        public async Task<ContentResult> Get()
        {
            try
            {
                var data = await GetAllPersonsAsync();
                _dataProcessor.SaveDataToDatabase(data);

               string jsonData = JsonConvert.SerializeObject(_allCustomers);

                return Content(jsonData, "application/json");
            }
            catch
              (Exception ex)
            {

                return null;
            }

        }
       
        [HttpPost]
        public IActionResult Post()
        {
            
            Role role1 = new Role();
            role1.Name = "user";
            role1.IsDefault = true;
            _context.Roles.Add(role1);
            _context.SaveChanges();

            Role role2 = new Role();
            role2.Name = "agent";
            role2.IsDefault = false;
            _context.Roles.Add(role2);
            _context.SaveChanges();
            

            Role role3 = new Role();
            role3.Name = "admin";
            role3.IsDefault = false;
            _context.Roles.Add(role3);
            _context.SaveChanges();

            RoleUseCase ruc1 = new RoleUseCase();
            ruc1.RoleId = role2.Id;
            ruc1.UseCaseId = 1;
            _context.RoleUseCases.Add(ruc1);
            _context.SaveChanges();

            RoleUseCase ruc2 = new RoleUseCase();
            ruc2.RoleId = role2.Id;
            ruc2.UseCaseId = 2;
            _context.RoleUseCases.Add(ruc2);
            _context.SaveChanges();

            RoleUseCase ruc3 = new RoleUseCase();
            ruc3.RoleId = role2.Id;
            ruc3.UseCaseId = 3;
            _context.RoleUseCases.Add(ruc3);
            _context.SaveChanges();

            string pass1 = "sifra123";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(pass1);

            User user1 = new User();
            user1.RoleId = role2.Id;
            user1.FirstName = "Petar";
            user1.LastName = "Peric";
            user1.Username = "pera.peric";
            user1.Password = passwordHash;
            user1.Email = "petar@gmail.com";
            _context.Users.Add(user1);
            _context.SaveChanges();

            
            Service service1 = new Service();
            service1.Name = "Wired Network";
            service1.TypeOfService = "Network";
            service1.Description = "Uses cables to connect devices, such as laptop or desktop computers, to the Internet or another network";
            _context.Services.Add(service1);
            _context.SaveChanges();
            

            Price price1 = new Price();
            price1.ServiceId = service1.Id;
            price1.ServicePrice = 16.21;
            _context.Prices.Add(price1);
            _context.SaveChanges();

            
            Service service2 = new Service();
            service2.Name = "Wireless Network";
            service2.TypeOfService = "Network";
            service2.Description = "A wireless network is a grouping, or network, of multiple devices where data is sent and received over radio frequencies";
            _context.Services.Add(service2);
            _context.SaveChanges();

            Price price2 = new Price();
            price2.ServiceId = service2.Id;
            price2.ServicePrice = 25.60; 
            _context.Prices.Add(price2);
            _context.SaveChanges();

            CustomerDiscount ud1 = new CustomerDiscount();
            ud1.CustomerId = 1;
            ud1.AgentId = user1.Id;
            ud1.DiscountPercentage = 15;
            _context.CustomerDiscounts.Add(ud1);
            _context.SaveChanges();

            CustomerDiscount ud2 = new CustomerDiscount();
            ud2.CustomerId = 2;
            ud2.AgentId = user1.Id;
            ud2.DiscountPercentage = 20;
            _context.CustomerDiscounts.Add(ud2);
            _context.SaveChanges();

            CustomerDiscount ud3 = new CustomerDiscount();
            ud3.CustomerId = 3;
            ud3.AgentId = user1.Id;
            ud3.DiscountPercentage = 15;
            _context.CustomerDiscounts.Add(ud3);
            _context.SaveChanges();

            CustomerDiscount ud4 = new CustomerDiscount();
            ud4.CustomerId = 4;
            ud4.AgentId = user1.Id;
            ud4.DiscountPercentage = 25;
            _context.CustomerDiscounts.Add(ud4);
            _context.SaveChanges();

            CustomerDiscount ud5 = new CustomerDiscount();
            ud5.CustomerId = 5;
            ud5.AgentId = user1.Id;
            ud5.DiscountPercentage = 15;
            _context.CustomerDiscounts.Add(ud5);
            _context.SaveChanges();

            Purchase p1 = new Purchase();
            p1.CustomerId = 1;
            p1.ServiceId = service1.Id;
            p1.ActiveTill = new DateTime(2025,02,20);
            p1.Status = "active";
            _context.Purchases.Add(p1);
            _context.SaveChanges();

            Purchase p2 = new Purchase();
            p2.CustomerId = 2;
            p2.ServiceId = service1.Id;
            p2.ActiveTill = new DateTime(2025, 02, 20);
            p2.Status = "active";
            _context.Purchases.Add(p2);
            _context.SaveChanges();

            Purchase p3 = new Purchase();
            p3.CustomerId = 3;
            p3.ServiceId = service1.Id;
            p3.ActiveTill = new DateTime(2025, 02, 20);
            p3.Status = "active";
            _context.Purchases.Add(p3);
            _context.SaveChanges();

            Purchase p4 = new Purchase();
            p4.CustomerId = 2;
            p4.ServiceId = service2.Id;
            p4.ActiveTill = new DateTime(2025, 02, 20);
            p4.Status = "active";
            _context.Purchases.Add(p4);
            _context.SaveChanges();

            Purchase p5 = new Purchase();
            p5.CustomerId = 2;
            p5.ServiceId = service1.Id;
            p5.ActiveTill = new DateTime(2025, 02, 20);
            p5.Status = "active";
            _context.Purchases.Add(p5);
            _context.SaveChanges();


            return StatusCode(201);

        }

        public static async Task<object> GetPersonByIdAsync(string id)
        {
            SoapService.SOAPDemoSoapClient client = new SoapService.SOAPDemoSoapClient();

            var person = await client.FindPersonAsync(id);


            if (person != null)
            {

                if (person is Employee employee)
                {

                    return new EmployeeDTO
                    {
                        Name = employee.Name,
                        Age = employee.Age,
                        SSN = employee.SSN,
                        DOB = employee.DOB,
                        Home = new AddressDTO
                        {
                            Zip = employee.Home.Zip,
                            City = employee.Home.City,
                            State = employee.Home.State,
                            Street = employee.Home.Street
                        },
                        Office = new AddressDTO
                        {
                            Zip = employee.Office.Zip,
                            City = employee.Office.City,
                            State = employee.Office.State,
                            Street = employee.Office.Street
                        },
                        Spouse = employee.Spouse != null ? new PersonDTO
                        {
                            Name = employee.Spouse.Name,
                            Age = employee.Spouse.Age,
                            SSN = employee.Spouse.SSN,
                            DOB = employee.Spouse.DOB,
                            Home = new AddressDTO
                            {
                                Zip = employee.Spouse.Home.Zip,
                                City = employee.Spouse.Home.City,
                                State = employee.Spouse.Home.State,
                                Street = employee.Spouse.Home.Street
                            },
                            Office = new AddressDTO
                            {
                                Zip = employee.Spouse.Office.Zip,
                                City = employee.Spouse.Office.City,
                                State = employee.Spouse.Office.State,
                                Street = employee.Spouse.Office.Street
                            },
                            FavoriteColors = employee.Spouse.FavoriteColors
                        } : null,
                        FavoriteColors = employee.FavoriteColors,
                        Title = employee.Title,
                        Salary = employee.Salary,
                        Notes = employee.Notes,
                        Picture = employee.Picture
                    };

                }
                else if (person is Person person1)
                {
                    return new PersonDTO
                    {
                        Name = person1.Name,
                        Age = person1.Age,
                        SSN = person1.SSN,
                        DOB = person1.DOB,
                        Home = new AddressDTO
                        {
                            Zip = person1.Home.Zip,
                            City = person1.Home.City,
                            State = person1.Home.State,
                            Street = person1.Home.Street
                        },
                        Office = new AddressDTO
                        {
                            Zip = person1.Office.Zip,
                            City = person1.Office.City,
                            State = person1.Office.State,
                            Street = person1.Office.Street
                        },
                        FavoriteColors = person1.FavoriteColors,
                        Spouse = person1.Spouse != null ? new PersonDTO
                        {
                            Name = person1.Spouse.Name,
                            Age = person1.Spouse.Age,
                            SSN = person1.Spouse.SSN,
                            DOB = person1.Spouse.DOB,
                            Home = new AddressDTO
                            {
                                Zip = person1.Spouse.Home.Zip,
                                City = person1.Spouse.Home.City,
                                State = person1.Spouse.Home.State,
                                Street = person1.Spouse.Home.Street
                            },
                            Office = new AddressDTO
                            {
                                Zip = person1.Spouse.Office.Zip,
                                City = person1.Spouse.Office.City,
                                State = person1.Spouse.Office.State,
                                Street = person1.Spouse.Office.Street
                            },
                            FavoriteColors = person1.Spouse.FavoriteColors
                        } : null,

                    };
                }
                else
                {
                    Console.WriteLine("Unexpected type");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Unable to retrive person with id {id}");
                return null;
            }
        }



        public static async Task<List<object>> GetAllPersonsAsync()
        {
            List<object> allPersons = new List<object>();

            int id = 1;

            while (true)
            {
                object person = await GetPersonByIdAsync(id.ToString());

                if (person != null)
                {
                    allPersons.Add(person);
                    id++;
                }
                else
                {
                    break;
                }
            }

            return allPersons;

        }

       

    

    }
}
