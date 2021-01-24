using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public enum TransactionType
    {
        [Description("Credit (Deposit Money)")]
        Deposit = 1,

        [Description("Debit (Withdrawal)")]
        Withdraw,

        [Description("Debit (Transfer)")]
        Transfer,

        [Description("Debit (Service Charge)")]
        ServiceCharge,

        [Description("Debit (BillPay)")]
        BillPay
    }

    public class Transaction
    {
        [Required]
        public int TransactionID { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountNumber { get; set; }
        public virtual Account DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public DateTime TransactionTimeUtc { get; set; }
    }
}
