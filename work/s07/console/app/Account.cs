using System;
using System.Text;
using System.Text.Json;

namespace BankApp
{
    class Account
    {
        public int Number { get; set; }
        public int Balance { get; set; }
        public string Label { get; set; }
        public int Owner { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize<Account>(this);
        }

        public override string ToString()
        {
            return String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", Number, Balance, Label, Owner) + Environment.NewLine +
                    "+----------+----------+----------+----------+";
        }
    }
}