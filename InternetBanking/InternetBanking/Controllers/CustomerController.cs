using InternetBanking.Data;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using InternetBanking.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
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

        public async Task<IActionResult> Deposit(int id) => View(await _context.Accounts.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Deposit(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);

            IsValidAmount(amount, account);

            await _transactionService.AddDepositTransactionAsync(id, amount).ConfigureAwait(false);


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);

            IsValidAmount(amount, account);

            await _transactionService.AddWithdrawTransactionAsync(id, amount).ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }

        private IActionResult IsValidAmount(decimal amount, Account account)
        {
            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (amount.HasMoreThanTwoDecimalPlaces())
                ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = amount;
                return View(account);
            }
            return new EmptyResult();
        }
    }
}
