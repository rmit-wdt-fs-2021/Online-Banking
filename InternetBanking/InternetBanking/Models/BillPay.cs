using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public enum BillPeriod
    {
        OnceOff,
        Monthly,
        Quarterly
    }

    public record BillPay
    {
        [Required]
        public int BillPayID { get; init; }

        [Required]
        public string Payee { get; init; }

        [Required]
        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Amount { get; init; }

        [Required]
        [DataType(DataType.Date), Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; init; }

        [Required]
        public BillPeriod BillPeriod { get; init; } = BillPeriod.OnceOff;

        [Required]
        public DateTime ModifyDate { get; set; }
    }
}
