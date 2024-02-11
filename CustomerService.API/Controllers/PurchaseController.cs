﻿using CustomerService.Application.UseCaseHandiling;
using CustomerService.Application.UseCases.Commands;
using CustomerService.Application.UseCases.DTO;
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
    public class PurchaseController : ControllerBase
    {
        private ICommandHandler _commandHandler;

        public PurchaseController(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // POST api/<PurchaseController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreatePurchaseDto dto, [FromServices] ICreatePurchaseCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

       
    }
}
