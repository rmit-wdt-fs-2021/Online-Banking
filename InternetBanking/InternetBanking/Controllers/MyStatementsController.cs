using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Data;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using X.PagedList;

namespace InternetBanking.Controllers
{
    public class MyStatementsController : Controller
    {
        private readonly McbaContext _context;
        private readonly ITransactionService _transactionService;
        private const string AccountSessionKey = "_AccountSessionKey";

        private int CustomerId => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public MyStatementsController(McbaContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View(new Account
            {
                CustomerID = CustomerId
            });
        }

        public async Task<IActionResult> IndexToViewTransaction(Account viewModel)
        {
            var type = viewModel.AccountType;
            // Lazy load customer
            Customer customer = await _context.Customers.Include(x => x.Accounts).
                                    FirstOrDefaultAsync(x => x.CustomerID == CustomerId);

            // Get customer accounts
            var account = customer.Accounts.FirstOrDefault(x=> x.AccountType == type);

            if (account == null)
            {
                //TODO add error page for this
                return NotFound();
            }
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            string accountJson = JsonConvert.SerializeObject(account, Formatting.Indented, serializerSettings);

            HttpContext.Session.SetString(AccountSessionKey, accountJson);

            return RedirectToAction(nameof(ViewTransactions));
        }

        public async Task<IActionResult> IndexToViewTransactions(int accountNumber)
        {
            var account = await _context.Accounts.FindAsync(accountNumber);

            if (account == null)
            {
                //TODO add error page for this
                return NotFound();
            }
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            string accountJson = JsonConvert.SerializeObject(account, Formatting.Indented, serializerSettings);

            HttpContext.Session.SetString(AccountSessionKey, accountJson);

            return RedirectToAction(nameof(ViewTransactions));
        }

        public async Task<IActionResult> ViewTransactions(int? page = 1)
        {
            const int pageSize = 4;
            var accountJson = HttpContext.Session.GetString(AccountSessionKey);
            if (accountJson == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            ViewBag.Account = account;

            var pagedList = await _context.Transactions.Where(x => x.AccountNumber == account.AccountNumber)
                .ToPagedListAsync(page, pageSize);
            return View(pagedList);
        }
    }
}

       
    
    

