namespace EatmEntityFramework.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Date { get; set; }
        public double Amount { get; set; }

        public int AccountId { get; set; }
        //public Account Account { get; set; }
    }
}