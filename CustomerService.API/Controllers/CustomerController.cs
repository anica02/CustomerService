using CustomerService.Application.UseCaseHandiling;
using CustomerService.Application.UseCases.Queries;
using CustomerService.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(_queryHandler.HandleQuery(query, search));
        }

      
    }
}
