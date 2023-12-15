using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MellonBank.Data;
using MellonBank.Areas.Identity.Data;
namespace MellonBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MellonBankDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MellonBankDbContextConnection' not found.");

            builder.Services.AddDbContext<MellonBankDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<MellonBankUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<MellonBankRole>()
                .AddRoleManager<RoleManager<MellonBankRole>>()
                .AddUserManager<UserManager<MellonBankUser>>()
                .AddEntityFrameworkStores<MellonBankDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.MapRazorPages();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MellonBankDbContext>();
                var userManager = services.GetRequiredService<UserManager<MellonBankUser>>();


                Seed(context, userManager).Wait();
            }

            app.Run();
        }


        public static async Task Seed(MellonBankDbContext context, UserManager<MellonBankUser> userManager)
        {
            if (!context.Users.Any())
            {
                MellonBankUser user = new MellonBankUser();
                user.FirstName = "Xeiristis";
                user.LastName = "Xeiristopoulos";
                user.Email = "xeiristis@mellon.com";
                user.AFM = "123456789";
                user.UserName = user.Email;
                user.RoleId = context.Roles.Where(x => x.Name == "Employee").Select(x => x.Id).SingleOrDefault();
                user.SecurityStamp = Guid.NewGuid().ToString();
                await userManager.CreateAsync(user, "1234Test!@#$");
                context.SaveChanges();
                var userId = await userManager.GetUserIdAsync(user);
                var newUser = context.Users.Where(x => x.Id == userId).FirstOrDefault();
                await userManager.AddToRoleAsync(newUser, "Employee");
                context.SaveChanges();
            }
        }
    }
}
