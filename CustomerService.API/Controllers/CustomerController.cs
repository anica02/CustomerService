using CsvHelper;
using CustomerService.Application.UseCaseHandiling;
using CustomerService.Application.UseCases.DTO;
using CustomerService.Application.UseCases.Queries;
using CustomerService.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
       
        private IQueryHandler _queryHandler;

        public CustomerController(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] CustomerSearch search, [FromServices] IGetCustomersWithSuccessfulPurchaseQuery query)
        {
            var result = _queryHandler.HandleQuery(query, search);

            var csvContent = ConvertDataToCsv(result);

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

           

            return File(memoryStream, "text/csv", "customers.csv");



        }

        private string ConvertDataToCsv(IEnumerable<CustomerPurchasesDto> dataList)
        {
            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Name,Age,SSN,DOB,Home.Zip,Home.City,Home.State,Home.Street,Office.Zip,Office.City,Office.State,Office.Street,FavoriteColors,Title,Salary,Notes,Picture,Purchases.ServiceId,Purchases.ServiceName,Purchases.Status,Purchases.DiscountUsed,Purchases.ActiveTill,Purchases.Price,DiscountPercentage");

            foreach (var item in dataList)
            {
                foreach (var purchase in item.Purchases)
                {
                    csvBuilder.AppendLine($"{item.Name},{item.Age},{item.SSN},{item.DOB},{item.Home.Zip},{item.Home.City},{item.Home.State},{item.Home.Street},{item.Office.Zip},{item.Office.City},{item.Office.State},{item.Office.Street},{string.Join("|", item.FavoriteColors)},{item.Title},{item.Salary},{item.Notes},{item.Picture},{purchase.ServiceId},{purchase.ServiceName},{purchase.Status},{purchase.DiscountUsed},{purchase.ActiveTill},{purchase.Price},{item.DiscountPercentage}");
                }
            }

            return csvBuilder.ToString();
        }


    }
}
