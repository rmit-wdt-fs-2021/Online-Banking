using AdminApi.Models;
using AdminApi.Models.DataManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly TransactionManager _repo;

        public AdminController(TransactionManager repo)
        {
            _repo = repo;
        }

        public IEnumerable<Transaction> GetTransactions(int accountNumber, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return null;
        }

        public void LockAccount(int accountNumber, int unlockwaitTimeInMinutes = 1)
        {

        }

        public void blockScheduledPay(int billPayID)
        {

        }
    }
}
