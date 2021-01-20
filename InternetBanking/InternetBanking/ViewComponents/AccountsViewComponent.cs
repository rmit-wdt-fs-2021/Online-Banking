using InternetBanking.Interfaces;
using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.ViewComponents
{
    [ViewComponent(Name = "Account")]
    public class AccountsViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;
        public AccountsViewComponent(IAccountService accontService)
        {
            _accountService = accontService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetAllAccountsAsync());
        }

        private Task<List<Account>> GetAllAccountsAsync()
        {
            return Task.FromResult(_accountService.GetAllAccountsAsync().Result);
        }
    }
}
