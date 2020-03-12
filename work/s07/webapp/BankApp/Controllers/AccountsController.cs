using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BankApp.Models;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        public AccountsController(JsonFileAccountService accountsService)
        {
            this.AccountsService = accountsService;
        }

        public JsonFileAccountService AccountsService { get; }
        
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(this.AccountsService.GetAccounts());
        }

        [HttpGet("{account}")]
        public IActionResult GetAccount([FromRoute] int account)
        {

            try{
                return Ok(JsonSerializer.Serialize(
                    AccountsService.GetAccounts().First(a => a.Number == account)));
            }
            catch{}

            return NotFound($"Requested account number {account} does not exist");
        }

        [HttpPut("transfer")]
        public IActionResult transferPut([FromBody] Transaction trans){
            string result = AccountsService.Transfer(trans);

            if (result.Length > 0)
                return Conflict(result);
            else
                return Ok();
        }

        [HttpPost("transfer")]
        public IActionResult transferPost([FromForm] Transaction trans){
            string result = AccountsService.Transfer(trans);

            if (result.Length > 0)
                return Conflict(result);
            else
                return Redirect("/");
        }
    }
}