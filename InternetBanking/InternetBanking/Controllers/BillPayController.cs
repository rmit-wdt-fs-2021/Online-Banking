using InternetBanking.Data;
using InternetBanking.Filters;
using InternetBanking.Models;
using InternetBanking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> IndexAsync()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(new BillPayViewModel
            {
                Customer = customer
            });
        }
    }
}
