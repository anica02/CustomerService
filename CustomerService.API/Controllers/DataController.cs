
using CustomerService.DataAccess;
using CustomerService.DataAccess.DTO;
using CustomerService.Domain.Entitites;
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
        private List<object> _allCustomers;

        public DataController(MyDataProcessor dataProcessor)
        {
            _dataProcessor = new MyDataProcessor();
           
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
