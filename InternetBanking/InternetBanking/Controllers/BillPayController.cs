using InternetBanking.Data;
using InternetBanking.Filters;
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

        public BillPayController(McbaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
