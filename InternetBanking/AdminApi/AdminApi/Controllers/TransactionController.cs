using AdminApi.Models;
using AdminApi.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionManager _repo;

        public TransactionController(TransactionManager repo)
        {
            _repo = repo;
        }

        public IEnumerable<Transaction> GetTransactions(int accountNumber, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _repo.GetTransactions(accountNumber, fromDate, toDate);
        }
    }
}
