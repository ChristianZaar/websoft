using System;
using System.Text.Json;

namespace BankApp.Models
{
    public class Account
    {
        public int Number { get; set; }
        public int Balance { get; set; }
        public string Label { get; set; }
        public int Owner { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Account>(this);
        
    }
}