using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UserLoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
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
                    model.LoginID, model.Password, model.RememberMe, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                    return View(model);
                }

            }
            return RedirectToAction("index", "home");

        }
    }
}
