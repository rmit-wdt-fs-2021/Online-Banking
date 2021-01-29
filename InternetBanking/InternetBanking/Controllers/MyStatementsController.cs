using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Data;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using X.PagedList;
using InternetBanking.Filters;

namespace InternetBanking.Controllers
{
    /// <summary>
    /// Code referenced from Matthew Bolger's Tut/Lab 07.
    /// </summary>
    public class MyStatementsController : Controller
    {
        private readonly McbaContext _context;
        private readonly ITransactionService _transactionService;
        private const string AccountSessionKey = "_AccountSessionKey";

        private int CustomerId => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        [AuthorizeCustomer]
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

        public async Task<IActionResult> IndexToViewTransaction(Account model)
        {
            // Eager load customer
            Customer customer = await _context.Customers.Include(x => x.Accounts).
                                    FirstOrDefaultAsync(x => x.CustomerID == CustomerId);

            // Get customer account by type.
            Account account = customer.Accounts.FirstOrDefault(x => x.AccountType == model.AccountType);

            if (account == null)
            {
                // TODO : show custom err page?
                return RedirectToAction(nameof(Index));
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

            var pagedList = await _context.Transactions
                .OrderByDescending(x => x.TransactionTimeUtc)
                .Where(x => x.AccountNumber == account.AccountNumber)
                .ToPagedListAsync(page, pageSize);
            return View(pagedList);
        }
    }
}
