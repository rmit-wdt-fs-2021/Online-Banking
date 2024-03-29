﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AdminApp.AutoGeneratedModel
{
    [Index(nameof(CustomerId), Name = "IX_Accounts_CustomerID")]
    public partial class Account
    {
        public Account()
        {
            BillPays = new HashSet<BillPay>();
            TransactionAccountNumberNavigations = new HashSet<Transaction>();
            TransactionDestinationAccountNumberNavigations = new HashSet<Transaction>();
        }

        [Key]
        public int AccountNumber { get; set; }
        public int AccountType { get; set; }
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }
        public DateTime ModifyDate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Accounts")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(BillPay.AccountNumberNavigation))]
        public virtual ICollection<BillPay> BillPays { get; set; }
        [InverseProperty(nameof(Transaction.AccountNumberNavigation))]
        public virtual ICollection<Transaction> TransactionAccountNumberNavigations { get; set; }
        [InverseProperty(nameof(Transaction.DestinationAccountNumberNavigation))]
        public virtual ICollection<Transaction> TransactionDestinationAccountNumberNavigations { get; set; }
    }
}
