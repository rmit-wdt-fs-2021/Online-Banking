using InternetBanking.Data;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Services
{
    public class AccountService : IAccountService
    {
        private readonly McbaContext _context;

        public AccountService(McbaContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountAsync(int accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
        }
    }
}
