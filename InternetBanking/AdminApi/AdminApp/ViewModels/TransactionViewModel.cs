using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.ViewModels
{
    public class TransactionViewModel
    {
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Display(Name = "From Date")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
