using AdminApi.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly UserAccountManager _repo;

        public UserAccountController(UserAccountManager repo)
        {
            _repo = repo;
        }

        // PUT api/userAccount/2100
        [HttpPut("{id}")]
        public async Task LockAccountAsync(int id)
        {
            await _repo.LockAccountAsync(id);
        }
    }
}
