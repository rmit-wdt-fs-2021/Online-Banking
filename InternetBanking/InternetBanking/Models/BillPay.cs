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
        [ForeignKey("Account")]
        public int AccountNumber { get; init; }
        public virtual Account Account { get; init; }

        [Required]
        public int PayeeID { get; init; }
        public virtual Payee Payee { get; init; }

        [Required]
        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Amount { get; init; }

        [Required]
        [DataType(DataType.Date), Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; init; }

        [Required]
        public BillPeriod Period { get; init; } = BillPeriod.OnceOff;

        [Required]
        public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
    }
}
