using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models
{
    public class EditAccount
    {

        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string ManagementBranch { get; set; }
    }
}
