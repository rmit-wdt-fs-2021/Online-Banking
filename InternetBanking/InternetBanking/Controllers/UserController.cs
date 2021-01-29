//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using InternetBanking.Services;
//using InternetBanking.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace InternetBanking.Controllers
//{
//    public class UserController : Controller
//    {
//        private UserService _userService;
//        public UserController(UserService userService)
//        {
//            _userService = userService;
//        }


//        [HttpPost("Register")]
//        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel registerViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                await _userService.RegisterUser(registerViewModel);
//                return null;
//            }
//            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
//            return View(ModelState);
//        }
//    }
//}
