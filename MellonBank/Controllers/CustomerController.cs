using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using MellonBank.Areas.Identity.Data;
using MellonBank.Data;
using MellonBank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace MellonBank.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {

        private readonly MellonBankDbContext _context;
        private readonly UserManager<MellonBankUser> _userManager;
        private readonly ILogger<CreateUser> _logger;
        private readonly SignInManager<MellonBankUser> _signInManager;

        public CustomerController(SignInManager<MellonBankUser> signInManager, MellonBankDbContext context, UserManager<MellonBankUser> userManager, ILogger<CreateUser> logger)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        // GET: CustomerController
        public ActionResult Index(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var users = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return View(users);
        }



        public async Task<IActionResult> ViewAccounts(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
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



        public ActionResult Details(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            return View(user);
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                // Password changed successfully
                return RedirectToAction("ChangePasswordConfirmation");
            }
            else
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        public ActionResult ChangePasswordConfirmation()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> AddMoney(string AccountNumber)
        {
            var account = await _context.MellonnBankAccounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            var viewModel = new AddMoneyViewModel();
            {
                viewModel.AccountNumber = account.AccountNumber;
            }
             return View(viewModel);
        }

            
        

        [HttpPost]
        public async Task<IActionResult> AddMoney(AddMoneyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var selectedAccount = await _context.MellonnBankAccounts.FirstOrDefaultAsync(a => a.AccountNumber == viewModel.AccountNumber);

                if (selectedAccount != null)
                {
                    selectedAccount.Balance += viewModel.Amount;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Customer");
                }
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> SendMoney(string AccountNumber)
        {
            var account = await _context.MellonnBankAccounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            var viewModel = new SendMoney();
            {
                viewModel.AccountNumber = account.AccountNumber;
            }
            return View(viewModel);
        }




        [HttpPost]
        public async Task<IActionResult> SendMoney(SendMoney viewModel)
        {
            if (ModelState.IsValid)
            {
                var selectedAccount = await _context.MellonnBankAccounts.FirstOrDefaultAsync(a => a.AccountNumber == viewModel.AccountNumber);
                
                if (selectedAccount != null)
                {
                    
                    selectedAccount.Balance -= viewModel.Ammout;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Customer");
                }
            }

            return View(viewModel);
        }


      
      

    }
}
