using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsReceivable.API.Models
{
    public class CustomerWalletInfo
    {
        [Key]
        public int CustomerWalletId { get; set; }
        public int CustomerId { get; set; }
        public int? TotalBusinessAmount { get; set; }
        public int? TotalPaidAmount { get; set; }
        public int? DueAmount { get; set; }
        public int? CreditLimit { get; set; }
    }
    public class CustomerWalletTransactionList
    {
        [Key]
        public int? TransactionId { get; set; }
        public int? TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionMode { get; set; }
        public string CardNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
