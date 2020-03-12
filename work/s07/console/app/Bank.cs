using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace BankApp
{
    class Bank
    {
        private StringBuilder sb = new StringBuilder();
        private const int minMenu = 1;
        private const int maxMenu = 6;
        private List<Account> accounts;
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true

        };

        public Bank()
        {
            accounts = ReadJson();
            sb = new StringBuilder();
            InitMenu();
        }

        public void Run()
        {
            int n = 0;

            while(n != maxMenu)
            {
                PrintMenu();
                n = GetInt(minMenu,maxMenu);
                DoMenuAction(n);
            }
        }

        #region Json handling
        /// <summary>
        /// Read json
        /// </summary>
        /// <returns></returns>
        private List<Account> ReadJson()
        {
            List<Account> accounts = null;
            //string file = "../data/account.json";
            string path = Directory.GetCurrentDirectory();
            string file = @"..\data\account.json";
            try
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string data = r.ReadToEnd();

                    accounts = JsonSerializer.Deserialize<List<Account>>(data,options);
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                    Console.WriteLine("Cannot find account file");
                if (e is DirectoryNotFoundException)
                    Console.WriteLine("Cannot find file directory");
                Environment.Exit(-1);
            }
            return accounts;
        }

        /// <summary>
        /// Write json file
        /// </summary>
        /// <param name="accounts"></param>
        private async void WriteJson(List<Account> accounts)
        {
            string file =  @"..\data\account.json";
            try
            {
                using (FileStream fs = File.Open(file, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, accounts, options);
                }
            }
            catch { };
            
        }
        #endregion

        /// <summary>
        /// Inits menu layout
        /// </summary>
        private void InitMenu()
        {
            sb.Append("+---------------------------------------+");
            sb.Append(Environment.NewLine);
            sb.Append("|\t1. View account\t\t\t|");
            sb.Append(Environment.NewLine);
            sb.Append("|\t2. View account by number\t|");
            sb.Append(Environment.NewLine);
            sb.Append("|\t3. Search\t\t\t|");
            sb.Append(Environment.NewLine);
            sb.Append("|\t4. Move\t\t\t\t|");
            sb.Append(Environment.NewLine);
            sb.Append("|\t5. New account\t\t\t|");
            sb.Append(Environment.NewLine);
            sb.Append("|\t6. Exit\t\t\t\t|");
            sb.Append(Environment.NewLine);
            sb.Append("+---------------------------------------+");
        }

        /// <summary>
        /// Prints menu on consol
        /// </summary>
        public void PrintMenu()
        {
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Executes action selected in menu
        /// </summary>
        /// <param name="n"></param>
        private void DoMenuAction(int n)
        {
            switch (n)
            {
                case 1:
                    PrintAccounts(accounts);
                    break;
                case 2:
                    PrintAccount();
                    break;
                case 3:
                    Search();
                    break;
                case 4:
                    Move();
                    break;
                case 5:
                    NewAccount();
                    break;
                case 6://Exit
                    break;
            }
        }

        /// <summary>
        /// Create new account 
        /// </summary>
        private void NewAccount()
        {
            string answer;
            Account a = new Account();

            do
            {
                a.Number = NewAccounNumber();
                a.Owner = NewAccounOwner();
                a.Label = NewAccountLabel();
                a.Balance = NewAccounBalance();
                PrintAccount(a);
                Console.WriteLine("Is your information correct? Y/N");
                answer = Console.ReadLine().ToLower();
            } while (!answer.Equals("y"));
            accounts.Add(a);
            WriteJson(accounts);
        }

        private int NewAccounBalance()
        {
            Console.WriteLine("Enter your balance.");
            return GetInt(0, 1000000);
        }

        private string NewAccountLabel()
        {
            Console.WriteLine("Enter a label.");
            return Console.ReadLine();
        }



        /// <summary>
        /// Get unique account number from user
        /// </summary>
        /// <returns></returns>
        private int NewAccounNumber()
        {
            do
            {
                Console.WriteLine("Enter a uniqe account number.");
                int n = GetInt(0, int.MaxValue);
                if (AccountNumberExists(n))
                    Console.WriteLine("Account number already exists.");
                else
                    return n;

            } while (true);
        }

        /// <summary>
        /// Get unique Owner from user
        /// </summary>
        /// <returns></returns>
        private int NewAccounOwner()
        {
            do
            {
                Console.WriteLine("Enter a uniqe owner number.");
                int n = GetInt(0, int.MaxValue);
                if (OwnerExists(n))
                    Console.WriteLine("Owner already exists.");
                else
                    return n;

            } while (true);
        }

        /// <summary>
        /// Checks if account exists
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool AccountNumberExists(int n)
        {
            return accounts.Any<Account>(a => a.Number == n);
        }

        /// <summary>
        /// Check if owner exists 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool OwnerExists(int n)
        {
            return accounts.Any<Account>(a => a.Owner == n);
        }

        /// <summary>
        /// Move balance from one account to another
        /// </summary>
        private void Move()
        {
            Console.WriteLine("Write account number to move balance from. ");
            int a, b, amount;
            //Get account numbers
            a = GetAccountNumber();
            Console.WriteLine($"{Environment.NewLine}Account nunmber {a} is selected to transfer from.");
            Console.WriteLine("Write account number to move balance to. ");
            b = GetAccountNumber();
            Console.WriteLine($"Account nunmber {b} is selected to transfer to.");
            //Get account to check balance
            Account acA = GetAccount(a);
            Account acB = GetAccount(b);
            Console.WriteLine($"Enter amount to transfer from account {a} to account {b}. Min to transfer is 0 max is {acA.Balance}.");
            amount = GetInt(0, acA.Balance);
            //transfer
            acA.Balance -= amount;
            acB.Balance += amount;
            //Save
            WriteJson(accounts);
        }

        /// <summary>
        /// Get int input from user on console
        /// </summary>
        /// <param name="min"></param> 
        /// <param name="max"></param>
        /// <returns></returns>
        private int GetInt(int min, int max)
        {
            int n;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out n) || n < min || n > max)
                    Console.WriteLine($"Invalid value. Minimum {min}, maximum {max}.");

                int dn=n;

            } while (n < min || n > max);

            return n;
        }

        /// <summary>
        /// Get account number from user
        /// </summary>
        /// <returns></returns>
        private int GetAccountNumber()
        {
            int a;

            do
            {
                Console.Write("Select account: ");
                a = PrintAccount();
                if (a < 0)
                    Console.WriteLine("invalid account number");
            } while (a < 0);
            return a;
        }

        /// <summary>
        /// Gets account 
        /// </summary>
        /// <param name="accountNbr"></param>
        /// <returns></returns>
        private Account GetAccount(int accountNbr)
        {
            Account account = null;
            try
            {
                account = accounts.First(a => a.Number == accountNbr);
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                    Console.WriteLine($"Account with account number {accountNbr} cannot be found.");
            }
            return account;
        }

        /// <summary>
        /// Search
        /// </summary>
        private void Search()
        {
            Console.Write("Input search word: ");
            string search = Console.ReadLine();
            var ac = accounts.Where(a =>
               a.Number.ToString().ToLower().Contains(search) ||
               a.Label.ToString().ToLower().Contains(search) ||
               a.Owner.ToString().ToLower().Contains(search) ||
               a.Balance.ToString().ToLower().Contains(search));
            PrintAccounts(ac.ToList());
        }

        /// <summary>
        /// Print all accounts
        /// </summary>
        /// <param name="accounts"></param>
        private void PrintAccounts(List<Account> accounts)
        {
            if (accounts.Count > 0)
                PrintTableHeader();
            else
                Console.WriteLine("No accounts found.");

            foreach (Account a in accounts)
            {
                Console.WriteLine(a.ToString());
            }
        }



        /// <summary>
        /// Print single account with input
        /// </summary>
        /// <returns></returns>
        private int PrintAccount()
        {
            Console.WriteLine("Enter account number");
            int accountNbr = GetInt(int.MinValue, int.MaxValue);
            Account account = GetAccount(accountNbr);
            return PrintAccount(account);
        }

        /// <summary>
        /// Print single account
        /// </summary>
        /// <returns></returns>
        private int PrintAccount(Account account)
        {
            int retval = -1;

            if (account != null)
            {
                retval = account.Number;
                PrintTableHeader();
                Console.WriteLine(account.ToString());
            }
            return retval;
        }


        /// <summary>
        /// Prints table header for accounts 
        /// </summary>
        private void PrintTableHeader()
        {
            Console.WriteLine(
                "+----------+----------+----------+----------+" + Environment.NewLine +
                String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", "Number", "Balance", "Label", "Owner") + Environment.NewLine +
                "+----------+----------+----------+----------+");
        }
    }
}