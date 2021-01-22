using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.ViewModels
{
    public class BillPayViewModel
    {
        [Required]
        public Customer Customer { get; set; }

        [Display(Name = "From Account")]
        public int FromAccount { get; set; }

        [Required, Display(Name = "To Payee")]
        public string ToPayee { get; set; }

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
