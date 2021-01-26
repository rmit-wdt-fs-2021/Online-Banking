using AdminApi.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillPayController : ControllerBase
    {
        private readonly BillPayManager _repo;

        public BillPayController(BillPayManager repo)
        {
            _repo = repo;
        }

        [HttpPut]
        public async Task BlockBillPayAsync(int billPayId)
        {
            await _repo.BlockBillPayAsync(billPayId).ConfigureAwait(false);
        }

        [HttpPut]
        public async Task UnblockBillPayAsync(int billPayId)
        {
            await _repo.UnblockBillPayAsync(billPayId).ConfigureAwait(false);
        }
    }
}
