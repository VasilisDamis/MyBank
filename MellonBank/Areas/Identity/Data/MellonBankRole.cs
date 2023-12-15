using Microsoft.AspNetCore.Identity;

namespace MellonBank.Areas.Identity.Data
{
    public class MellonBankRole : IdentityRole
    {
        public virtual ICollection<MellonBankUser> Users { get; set; }
    }
}
