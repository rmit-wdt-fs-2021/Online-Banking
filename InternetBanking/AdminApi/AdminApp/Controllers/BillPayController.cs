using AdminApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Controllers
{
    public class BillPayController : Controller
    {
        private readonly ILogger<BillPayController> _logger;
        private readonly IBillPayService _billPayService;

        public BillPayController(IBillPayService billPayService, ILogger<BillPayController> logger)
        {
            _billPayService = billPayService;
            _logger = logger;
        }


    }
}
