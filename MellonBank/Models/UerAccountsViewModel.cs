using MellonBank.Areas.Identity.Data;

namespace MellonBank.Models
{
    public class UerAccountsViewModel
    {
        public string UserId { get; set; }
        public List<MellonnBankAccounts> MellonnBankAccounts { get; set; }
    }
}
