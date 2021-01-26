using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    public class ErrorCodeController : Controller
    {
        //Taken from lecture 7

        [HttpGet("ErrorCode/{errorCode}")]
        public IActionResult Index(int errorCode)
        {
            return View(errorCode);
        }
    }
}
