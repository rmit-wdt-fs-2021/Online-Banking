using AdminApp.Interfaces;
using AdminApp.ViewModels;
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
        private readonly ICustomerService _customerService;
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(ILogger<UserAccountController> logger, ICustomerService customerService, IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
            _customerService = customerService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(UserAccountViewModel viewModel)
        {
            viewModel.Customers = await _customerService.GetAllCustomersAsync().ConfigureAwait(false);
            if (viewModel.CustomerID == 0)
            {
                return View(viewModel);
            }

            await _userAccountService.LockAccountAsync(viewModel.CustomerID).ConfigureAwait(false);
            return View(viewModel);
        }

        private void LockAccount(int customerID)
        {
        }
    }
}
