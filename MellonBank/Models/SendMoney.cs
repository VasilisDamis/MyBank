namespace MellonBank.Models
{
    public class SendMoney
    {
        public string AccountNumber { get; set; }

        public string ToAccountNumber { get; set; }

        public Decimal Ammout { get; set; }


    }
}
