using InternetBanking.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.ViewModels
{
    public class BillPayViewModel
    {
        [Display(Name = "From Account")]
        public Account FromAccount { get; set; }

        [Required, Display(Name = "To Payee")]
        public string ToPayee { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required , Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public BillPeriod BillPeriod { get; set; }
    }
}
