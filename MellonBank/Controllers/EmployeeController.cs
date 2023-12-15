using System.Runtime.InteropServices;
using System.Security.Principal;
using MellonBank.Areas.Identity.Data;
using MellonBank.Data;
using MellonBank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Controllers
{
    [Authorize(Roles = "Employee")]


    public class EmployeeController : Controller
    {


        private readonly SignInManager<MellonBankUser> _signInManager;
        private readonly UserManager<MellonBankUser> _userManager;
        private readonly IUserStore<MellonBankUser> _userStore;
        private readonly IUserEmailStore<MellonBankUser> _emailStore;
        private readonly ILogger<CreateUser> _logger;
        private readonly IEmailSender _emailSender;
        private readonly MellonBankDbContext _context;


        public EmployeeController(MellonBankDbContext context,
            UserManager<MellonBankUser> userManager,
            IUserStore<MellonBankUser> userStore,
            SignInManager<MellonBankUser> signInManager,
            ILogger<CreateUser> logger)

        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;

        }
        // GET: EmployeeControler
        public ActionResult Index()
        {
            return View(_context.Users.Where(x => x.RoleId == "2"));

        }


        public async Task<IActionResult> ViewAccounts(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            //return View(_context.MellonnBankAccounts.Where(x => x.Users.Id == id).ToList());
            var model = new UerAccountsViewModel
            {
                UserId = user.LastName,
                MellonnBankAccounts = _context.MellonnBankAccounts
            .Where(x => x.Users.Id == id)
            .ToList()
            };

            return View(model);

        }


        [HttpGet]
        public ActionResult CreateNewUser()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateNewUser(CreateUser create)
        {
            if (ModelState.IsValid)
            {
                var user = new MellonBankUser();
                user.FirstName = create.FirstName;
                user.LastName = create.LastName;
                user.Address = create.Address;
                user.AFM = create.AFM;
                user.UserName = create.Email;
                user.PhoneNumber = create.Phone;
                user.Email = create.Email;
                user.RoleId = _context.Roles.Where(x => x.Name == "Customer").Select(x => x.Id).SingleOrDefault();
                user.SecurityStamp = Guid.NewGuid().ToString();
                await _userManager.CreateAsync(user, create.Password);
                _context.SaveChanges();
                var userId = await _userManager.GetUserIdAsync(user);
                var newUser = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
                await _userManager.AddToRoleAsync(newUser, "Customer");
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(create);
        }



      

            // GET: EmployeeControler/Details/5

            public ActionResult Result(string id)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Id == id); 

            return View(user);
        }

       


        // GET: EmployeeControler/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("Error");
            }
            var model = new EditUser();
            { 
            model.Id = user.Id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.AFM = user.AFM;
            model.Address = user.Address;
            model.Phone = user.PhoneNumber;
            model.Email = user.Email;
            }

            return View(model);
        }

        // POST: EmployeeControler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser( EditUser edit)
        {
            var user = await _userManager.FindByIdAsync(edit.Id);
            if (ModelState.IsValid) 
            {   
                user.Email = edit.Email;
                user.FirstName = edit.FirstName;
                user.LastName = edit.LastName;
                user.AFM = edit.AFM;
                user.Address = edit.Address;
                user.PhoneNumber = edit.Phone;


                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(edit);
        }

        
        //DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            var itemToDelete = await _context.Users.FindAsync(id);

            if (itemToDelete == null)
            {
                return View("Error");
            }

            return View(itemToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var itemToDelete = await _context.Users.FindAsync(id);

            if (itemToDelete == null)
            {
                return View("Error");
            }

            _context.Users.Remove(itemToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult SearchUsers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchUsers(string LastName)
        {
            return View(_context.Users.Where(x => x.LastName == LastName));
           
        }



        [HttpGet]
        public async Task<IActionResult> CreateAccount(string id) 
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return View("Error");
            }
            var model = new CreateAccount();
            {   
                
                model.AFM = user.AFM;
                model.AccountNumber = RandomAccNumber();
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccount account)
        {
            if (ModelState.IsValid) 
            {     
               
                    var NewAccount = new MellonnBankAccounts();
                    NewAccount.AFM = account.AFM;
                    NewAccount.AccountNumber = account.AccountNumber;
                    NewAccount.Balance = account.Balance;
                    NewAccount.AccountType = account.AccountType; 
                    NewAccount.ManagementBranch = account.ManagementBranch;
                if (NewAccount != null)
                {


                    _context.Add(NewAccount);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }   
            
            return View();
        }

                     


        public string RandomAccNumber()
        {
            var random = new Random();
            var userexists = _context.MellonnBankAccounts.Any(x => x.AccountNumber == random.ToString());
            if ( userexists)
            {   
                random = new Random();
                
            }

            string randomAccountNumber = "MELLON" + random.Next(925600000, 925610000).ToString();
            return randomAccountNumber;
        }



        [HttpGet]
        public async Task<IActionResult> EditAccount(string AccountNumber)
        {

            var user = await _context.MellonnBankAccounts.Where(x=>x.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            if (user == null)
            {
                return View("Error");
            }
            var model = new EditAccount();
            {
                model.AccountNumber= user.AccountNumber;
                model.AccountType = user.AccountType;
                model.ManagementBranch = user.ManagementBranch;
                
            }
            return View(model);
        }

        // POST: EmployeeControler/EditAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(EditAccount edit)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.MellonnBankAccounts
                    .Where(x => x.AccountNumber == edit.AccountNumber)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return View("Error");
                }

                // Update the user with the edited values
                user.AccountType = edit.AccountType;
                user.ManagementBranch = edit.ManagementBranch;

                // Save the changes to the database
                _context.MellonnBankAccounts.Update(user);
                await _context.SaveChangesAsync();

                // Redirect to a different action or return a success view
                return RedirectToAction("Index"); // Replace "Index" with the desired action
            }

            // If ModelState is not valid, return the edit view with the provided model
            return View(edit);
        }



        //DELETE
        [HttpGet]
        public async Task<IActionResult> DeleteAccount(string AccountNumber)
        {

            var itemToDelete =  await _context.MellonnBankAccounts
                    .Where(x => x.AccountNumber == AccountNumber)
                    .FirstOrDefaultAsync();

            if (itemToDelete == null)
            {
                return View("Error");
            }

            return View(itemToDelete);
        }

        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccountConfirmed(string AccountNumber)
        {
            var itemToDelete = await _context.MellonnBankAccounts.FindAsync(AccountNumber);

            if (itemToDelete == null)
            {
                return View("Error");
            }

            _context.MellonnBankAccounts.Remove(itemToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }





    }


}

