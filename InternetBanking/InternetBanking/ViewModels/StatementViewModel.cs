using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.ViewModels
{
    public class StatementViewModel
    {
        public int CustomerID { get; set; }
        public int AccountNumber { get; set; }

        public AccountType AccountType { get; set; }
        
    }
}
