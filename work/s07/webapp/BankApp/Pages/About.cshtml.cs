using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BankApp.Services;
using System.Collections.Generic;
using BankApp.Models;

namespace BankApp.Pages
{
    public class AboutModel : PageModel
    {

        private readonly ILogger<AboutModel> _logger;

        public AboutModel(
            ILogger<AboutModel> logger,
            JsonFileAccountService jsonService)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}