﻿using InternetBanking.Data;
using InternetBanking.Exceptions;
using InternetBanking.Filters;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using InternetBanking.Utilities;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
    [AuthorizeCustomer]
    public class CustomerController : Controller
    {
        private readonly McbaContext _context;
        private readonly ITransactionService _transactionService;
        private readonly ILogger<CustomerController> _logger;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public CustomerController(McbaContext context, ITransactionService transactionService, ILogger<CustomerController> logger)
        {
            _context = context;
            _transactionService = transactionService;
            _logger = logger;
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

            try
            {
                await _transactionService.AddDepositTransactionAsync(viewModel.Account, viewModel.Amount).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong and deposit was unsuccessful: ");
                _logger.LogError(e.Message);
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

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

            try
            {
                await _transactionService.AddWithdrawTransactionAsync(account, amount).ConfigureAwait(false);
            }
            catch (AccountBalanceUpdateException)
            {
                ModelState.AddModelError(nameof(amount), "It appears you do not have enough funds in this account for this transaction.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong and withdraw was unsuccessful: ");
                _logger.LogError(e.Message);
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Transfer(int accountNumber)
        {
            return View(new TransferViewModel
            {
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

            var srcAccount = viewModel.Account;
            var destAccount = await _context.Accounts.FindAsync(viewModel.ToAccountNumber);
            var amt = viewModel.Amount;

            if (srcAccount is null)
            {
                ModelState.AddModelError(string.Empty, "Unable to find account with id.");
                return View(viewModel);
            }

            if (destAccount is null)
            {
                ModelState.AddModelError(nameof(toAccountNumber), "Unable to find account with id.");
                return View(viewModel);
            }

            if (srcAccount.AccountNumber == destAccount.AccountNumber)
            {
                ModelState.AddModelError(string.Empty, "Source and destination accounts cannot be the same.");
                return View(viewModel);
            }

            if (!IsValidAmount(amt))
            {
                return View(viewModel);
            }

            try
            {
                await _transactionService.AddTransferTransactionAsync(srcAccount, destAccount, amt, viewModel.Comment).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            catch (AccountBalanceUpdateException)
            {
                ModelState.AddModelError(nameof(amount), "It appears you do not have enough funds in this account for this transaction.");
            }
            catch (AggregateException e)
            {
                if (e.InnerException is AccountBalanceUpdateException)
                {
                    ModelState.AddModelError(nameof(amount), "It appears you do not have enough funds in this account!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong and transfer was unsuccessful!");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

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
