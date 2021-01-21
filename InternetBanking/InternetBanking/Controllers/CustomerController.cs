using InternetBanking.Data;
using InternetBanking.Filters;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using InternetBanking.Utilities;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
    [AuthorizeCustomer]
    public class CustomerController : Controller
    {
        private readonly McbaContext _context;
        private readonly ITransactionService _transactionService;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public CustomerController(McbaContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customers.FindAsync(CustomerID);

            // OR
            // Eager loading.
            //var customer = await _context.Customers.Include(x => x.Accounts).
            //    FirstOrDefaultAsync(x => x.CustomerID == _customerID);

            return View(customer);
        }

        public async Task<IActionResult> Deposit(int accountNumber)
        {
            return View(await CreateATMViewModelAsync(accountNumber).ConfigureAwait(false));
        }

        private async Task<ATMViewModel> CreateATMViewModelAsync(int accountNumber)
        {
            return new ATMViewModel
            {
                AccountNumber = accountNumber,
                Account = await _context.Accounts.FindAsync(accountNumber)
            };
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(ATMViewModel viewModel)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);
            var account = viewModel.Account;
            var amount = viewModel.Amount;

            if (!IsValidAmount(amount))
            {
                return View(viewModel);
            }

            await _transactionService.AddDepositTransactionAsync(account, amount).ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Withdraw(int accountNumber)
        {
            return View(await CreateATMViewModelAsync(accountNumber).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(ATMViewModel viewModel)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);
            var account = viewModel.Account;
            var amount = viewModel.Amount;

            if (!IsValidAmount(amount))
            {
                return View(viewModel);
            }

            await _transactionService.AddWithdrawTransactionAsync(account, amount).ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Transfer(int accountNumber)
        {
            return View(new TransferViewModel {
                FromAccountNumber = accountNumber,
                Account = await _context.Accounts.FindAsync(accountNumber),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int toAccountNumber, decimal amount, TransferViewModel viewModel, string comment = null)
        {
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.FromAccountNumber);
            viewModel.ToAccountNumber = toAccountNumber;
            viewModel.Comment = comment;
            viewModel.Amount = amount;
            //viewModel.ToAccount = await _context.Accounts.FindAsync(viewModel.FromAccountNumber);

            var srcAccount = viewModel.Account;
            var destAccount = await _context.Accounts.FindAsync(viewModel.ToAccountNumber);
            var amt = viewModel.Amount;

            if (srcAccount is null)
            {
                ModelState.AddModelError(nameof(srcAccount), "Unable to find account with id.");
            }

            if(destAccount is null)
            {
                ModelState.AddModelError(nameof(destAccount), "Unable to find account with id.");
            }

            if(srcAccount.AccountNumber == destAccount.AccountNumber)
            {
                ModelState.AddModelError(nameof(viewModel.ToAccountNumber), "Source and destination accounts cannot be the same.");
            }

            if (!IsValidAmount(amt) || !ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _transactionService.AddTransferTransactionAsync(srcAccount, destAccount, amt, viewModel.Comment);

            return RedirectToAction(nameof(Index));
        }

        private bool IsValidAmount(decimal amount)
        {
            if (amount <= 0)
            {
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            }

            if (amount.HasMoreThanTwoDecimalPlaces())
            {
                ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            }

            return ModelState.IsValid;
        }
    }
}
