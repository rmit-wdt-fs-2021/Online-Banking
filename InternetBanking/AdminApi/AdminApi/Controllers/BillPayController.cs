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

        [HttpPut("{id}/{blockFlag}")]
        public async Task BlockBillPayAsync(int id, bool blockFlag)
        {
            await _repo.BlockBillPayAsync(id, blockFlag).ConfigureAwait(false);
        }
    }
}
