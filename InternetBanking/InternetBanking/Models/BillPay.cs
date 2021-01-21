using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public enum BillPeriod
    {
        OneTime,
        Annual
    }

    public record BillPay
    {
        public int BillPayID { get; init; }

        [Required]
        public string Payee { get; init; }

        [Required]
        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Amount { get; init; }

        [Required]
        [DataType(DataType.Date), Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; init; }

        public BillPeriod BillPeriod { get; init; } = BillPeriod.OneTime;
    }
}
