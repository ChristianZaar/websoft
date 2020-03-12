namespace BankApp.Models
{
    public class Transaction
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int Amount { get; set; }
    }
}