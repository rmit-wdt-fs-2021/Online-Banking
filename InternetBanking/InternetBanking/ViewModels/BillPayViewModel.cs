using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.ViewModels
{
    public class BillPayViewModel
    {
        public int BillPayID { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "From Account")]
        public int FromAccountNumber { get; set; }

        [Required, Display(Name = "To Payee")]
        public int ToPayeeID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; } = DateTime.Now;

        [Required]
        public BillPeriod Period { get; set; }

        public List<Payee> Payees { get; set; }
    }
}
