using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BankApp.Models;
using Microsoft.AspNetCore.Hosting;

namespace BankApp.Services
{
    public class JsonFileAccountService
    {
        public JsonFileAccountService(IWebHostEnvironment webHostEnvirnoment)
        {
            WebHostEnvironment = webHostEnvirnoment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string AccountsPath
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "account.json"); }
        }

        /// <summary>
        /// Reads accounts from file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAccounts()
        {
            using(var jsonFileReader = File.OpenText(AccountsPath))
            {
                return JsonSerializer.Deserialize<Account[]>(jsonFileReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Transfers money from one account to another
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public string Transfer(Transaction trans)
        {
            string errors = CheckTransferErrors(trans);

            if(errors.Length == 0)
            {
                var accounts = GetAccounts();
                Account sender = GetAccount(accounts, trans.FromAccount);
                Account receiver = GetAccount(accounts, trans.ToAccount);
                sender.Balance -= trans.Amount;
                receiver.Balance += trans.Amount;
                SaveAccounts(accounts);
                return "";
            }
            return errors;
        }

        /// <summary>
        /// Writes accounts to file
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        private async void SaveAccounts(IEnumerable<Account> accounts)
        {
            try
            {
                using (FileStream fs = File.Open(AccountsPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, accounts);
                }
            }
            catch { };
            
        }

        /// <summary>
        /// Finds account by account number or throws error
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Account GetAccount(IEnumerable<Account> accounts, int id)
        {
            return accounts.First(a => a.Number == id);
        }

        /// <summary>
        ///Checks for errors when making transfer between accounts 
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        private string CheckTransferErrors(Transaction trans){
            var accounts = GetAccounts();
            string errorStart = "\"Errors\" : [";
            string errorEnd = "]";
            string errors = "";
            int id = 0;

            try
            {
                Account sender = GetAccount(accounts, trans.FromAccount);
                //Check amounts when account exists
                if(trans.Amount > sender.Balance)
                    errors = AddError( errors, $"Error id:{++id} - Insufficient funds. Balance in account {trans.FromAccount} is {sender.Balance}");
            }
            catch
            {
                errors = AddError( errors, $"Error id:{++id} - Sending account {trans.FromAccount} does not exist");
            }
            if( !accounts.Any(a => a.Number == trans.ToAccount))
                errors = AddError( errors, $"Error id:{++id} - Receiving account {trans.ToAccount} does not exist");
            if( trans.ToAccount == trans.FromAccount)
                errors = AddError( errors, $"Error id:{++id} - Sender and recipient can not be the same account");
            if(errors.Length != 0)
            {
                errors = $"{{{errorStart}{errors}{errorEnd}}}"; 
            }

            return errors;
        }

        /// <summary>
        /// Adds error to error array
        /// </summary>
        /// <param name="error"></param>
        /// <param name="newError"></param>
        /// <returns></returns>
        private string AddError(string error, string newError)
        {
            //Separate array
            if(error.Length != 0)
                error += ",";
            
            return $"{error}\"{newError}\"";
        }

    }
}