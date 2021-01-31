using InternetBanking.Data;
using InternetBanking.Models;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
    /// <summary>
    /// Code referenced from : https://csharp-video-tutorials.blogspot.com/2019/06/implementing-login-functionality-in.html
    /// </summary>

    [Route("/Mcba/SecureUserLogin")]
    public class UserLoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly McbaContext _context;
        private readonly ILogger<UserLoginController> _logger;

        public UserLoginController(SignInManager<IdentityUser> signInManager, McbaContext context, ILogger<UserLoginController> logger)
        {
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [Route("LogoutUser")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
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
                    model.LoginID, model.Password, false, false);
                var login = await _context.Logins.FindAsync(model.LoginID);

                if (!result.Succeeded || login.IsLocked)
                {
                    ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                    return View(model);
                }

            }
            // Login customer.
            // Use username to get customer Id.
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == model.LoginID);
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), customer.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), customer.Name);

            return RedirectToAction("Index", "Customer");
        }
    }
}
