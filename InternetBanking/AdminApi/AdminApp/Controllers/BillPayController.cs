using AdminApp.Filters;
using AdminApp.Interfaces;
using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AdminApp.Controllers
{
    [AuthorizeAdmin]
    public class BillPayController : Controller
    {
        private readonly ILogger<BillPayController> _logger;
        private readonly IBillPayService _billPayService;

        public BillPayController(IBillPayService billPayService, ILogger<BillPayController> logger)
        {
            _billPayService = billPayService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(BillPayViewModel viewModel)
        {
            viewModel.BillPays = await _billPayService.GetAllBillPaysAsync().ConfigureAwait(false);
            if(viewModel.BillPayID == 0)
            {
                return View(viewModel);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Block(int id)
        {
            await _billPayService.BlockBillPayAsync(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Unblock(int id)
        {
            await _billPayService.UnblockBillPayAsync(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }
    }
}
