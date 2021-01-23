using InternetBanking.Data;
using InternetBanking.Filters;
using InternetBanking.Models;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
    [AuthorizeCustomer]
    public class BillPayController : Controller
    {
        private readonly McbaContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public BillPayController(McbaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            var payees = await _context.Payees.ToListAsync();
            
            return View(new BillPayViewModel
            {
                Customer = customer,
                Payees = payees
            });
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillPayViewModel viewModel)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            viewModel.Customer = customer;
            var billPay = new BillPay
            {
                PayeeID = viewModel.ToPayeeID,
                AccountNumber = viewModel.FromAccountNumber,
                Amount = viewModel.Amount,
                ScheduledDate = viewModel.ScheduledDate,
                Period = viewModel.Period,
                ModifyDate = DateTime.UtcNow
            };
            if (ModelState.IsValid)
            {
                _context.Add(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(billPay);
        }

        public async Task<IActionResult> DisplayBillPays()
        {
            var billPays = await _context.BillPay.ToListAsync();
            return View(new BillPayListViewModel
            {
                BillPays = billPays
            });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPay.FindAsync(id);
            if(billPay is null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(CustomerID);
            var payees = await _context.Payees.ToListAsync();
            return View(new BillPayViewModel
            {
                Customer = customer,
                Payees = payees,
                Amount = billPay.Amount,
                BillPayID = billPay.BillPayID
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BillPayViewModel viewModel)
        {
            if(id != viewModel.BillPayID)
            {
                return NotFound();
            }

            var billPay = await _context.BillPay.AsNoTracking().FirstOrDefaultAsync(x => x.BillPayID == id);
            billPay.AccountNumber = viewModel.FromAccountNumber;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillPayExists(billPay.BillPayID))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        private bool BillPayExists(int id) => _context.BillPay.Any(e => e.BillPayID == id);

        private BillPay CreateBillPay(BillPayViewModel viewModel)
        {
            return new BillPay
            {
                BillPayID = viewModel.BillPayID,
                PayeeID = viewModel.ToPayeeID,
                AccountNumber = viewModel.FromAccountNumber,
                Amount = viewModel.Amount,
                ScheduledDate = viewModel.ScheduledDate,
                Period = viewModel.Period,
                ModifyDate = DateTime.UtcNow
            };
        }
    }
}
