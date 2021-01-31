using System;
using InternetBanking.Data;
using InternetBanking.Models;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
    /// <summary>
    /// Code referenced from https://csharp-video-tutorials.blogspot.com/2019/06/aspnet-core-identity-usermanager-and.html
    /// </summary>

    public class UserAccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly McbaContext _context;

        public UserAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, McbaContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    UserName = model.LoginID,
                    Email = model.Email
                };

                // Store user data in AspNetUsers database table
                var result = await _userManager.CreateAsync(user, model.Password);


                // If user is successfully created, redirect to index action of HomeController
                if (result.Succeeded)
                {
                    var customer = await _context.Customers.FindAsync(model.CustomerID).ConfigureAwait(false);

                    if(customer == null)
                    {
                        await AddCustomerAsync(model).ConfigureAwait(false);
                    }

                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.LoginID, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }


        private async Task AddCustomerAsync(RegisterViewModel model)
        {
            var customer = new Customer
            {
                CustomerID = model.CustomerID,
                Name = model.Name,
                TFN = model.TFN,
                Address = model.Address,
                City = model.City,
                State = model.State,
                PostCode = model.PostCode,
                Phone = model.Phone,
                Username = model.LoginID
            };

            //Adding an account automatically to allow usage when registering
            var account = new Account
            {
                AccountNumber = new Random(DateTime.UtcNow.Millisecond).Next(4500,9999),
                AccountType = AccountType.Saving,
                CustomerID = model.CustomerID,
                Balance = 500,
                ModifyDate = DateTime.UtcNow
            };

            await _context.AddAsync(customer).ConfigureAwait(false);
            await _context.AddAsync(account).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}

