using AdminApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Data
{
    public class McbaContext : DbContext
    {
        public McbaContext(DbContextOptions<McbaContext> options) : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BillPay> BillPay { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}
