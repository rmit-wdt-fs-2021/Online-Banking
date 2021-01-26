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

        [HttpPut("{id}")]
        public async Task BlockBillPayAsync(int id)
        {
            await _repo.BlockBillPayAsync(id).ConfigureAwait(false);
        }

        [HttpPut("{id}")]
        public async Task UnblockBillPayAsync(int id)
        {
            await _repo.UnblockBillPayAsync(id).ConfigureAwait(false);
        }
    }
}
