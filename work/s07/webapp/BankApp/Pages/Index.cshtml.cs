using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BankApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<AboutModel> _logger;
        public JsonFileAccountService AccountService;
        public IEnumerable<Account> Accounts { get; private set;} 

        public IndexModel(
            ILogger<AboutModel> logger,
            JsonFileAccountService jsonService)
        {
            _logger = logger;
            AccountService = jsonService;
        }

        public void OnGet()
        {
            Accounts = AccountService.GetAccounts();
        }
    }
}
