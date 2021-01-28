using AdminApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    [Route("/Mcba/SecureAdminLogin")]
    public class LoginController : Controller
    {

        private const string AdminUsername = "admin";
        private const string AdminPassword= "admin";

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminPassword && password == AdminPassword)
            {
                HttpContext.Session.SetString(nameof(Admin.Username), AdminUsername);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { Username = username });
            }
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout admin.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
