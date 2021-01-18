using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Data
{
    public class McbaContext : DbContext
    {
        public McbaContext(DbContextOptions<McbaContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
