using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MellonBank.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MellonBankUser class
public class MellonBankUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string AFM { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    [ForeignKey("Role")]
    public string? RoleId { get; set; } = string.Empty;
    public virtual MellonBankRole? Role { get; set; }

    public virtual ICollection<MellonnBankAccounts> Accounts { get; set; }
}

