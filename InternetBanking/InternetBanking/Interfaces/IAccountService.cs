using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Interfaces
{
    public interface IAccountService
    {
        public Task<List<Account>> GetAllAccountsAsync();
    }
}
