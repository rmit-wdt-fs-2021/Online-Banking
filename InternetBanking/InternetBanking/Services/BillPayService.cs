using InternetBanking.Data;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Services
{
    public class BillPayService : IBillPayService
    {
        private readonly McbaContext _context;

        public BillPayService(McbaContext context)
        {
            _context = context;
        }

        public async Task DeleteBillPayAsync(BillPay billPay)
        {
            if (billPay is null)
            {
                throw new ArgumentNullException(nameof(billPay));
            }


        }
    }
}
