namespace MellonBank.Areas.Identity.Data
{
    public class MellonnBankAccounts
    {
        public string AccountNumber { get; set; } = string.Empty;

        public string AFM { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public string ManagementBranch { get; set; } = string.Empty;

        public string AccountType { get; set; } = string.Empty;

        public virtual MellonBankUser Users { get; set; }
    }
}
