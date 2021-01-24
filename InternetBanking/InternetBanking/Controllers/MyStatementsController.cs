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

        public MyStatementsController(McbaContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
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
                //return RedirectToAction(nameof(CustomerController.Index));
            }

            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            ViewBag.Account = account;

            var pagedList = await _context.Transactions.Where(x => x.AccountNumber == account.AccountNumber)
                .ToPagedListAsync(page, pageSize);
            return View(pagedList);
        }
    }
}

       
    
    

