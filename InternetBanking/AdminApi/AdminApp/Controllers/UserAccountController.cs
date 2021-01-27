using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly ILogger<UserAccountController> _logger;

        public UserAccountController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
