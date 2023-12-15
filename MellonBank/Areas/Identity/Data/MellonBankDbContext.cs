using MellonBank.Areas.Identity.Data;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Data;

public class MellonBankDbContext : IdentityDbContext<
        MellonBankUser, MellonBankRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    
    public DbSet<MellonnBankAccounts> MellonnBankAccounts { get; set; }
    public MellonBankDbContext(DbContextOptions<MellonBankDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MellonnBankAccounts>().HasKey(a => a.AccountNumber);
        builder.Entity<MellonnBankAccounts>()
        .HasOne(a => a.Users)
        .WithMany(u => u.Accounts)
        .HasForeignKey(a => a.AFM)
        .HasPrincipalKey(u => u.AFM);
        builder.Entity<MellonnBankAccounts>()
       .Property(a => a.Balance)
       .HasColumnType("decimal(18, 2)");

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<MellonBankUser>()
           .HasOne(x => x.Role)
        .WithMany(x => x.Users)
           .OnDelete(DeleteBehavior.NoAction);
    }
}
