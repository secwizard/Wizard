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
        public decimal? TotalBusinessAmount { get; set; }
        public decimal? TotalPaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? CreditLimit { get; set; }
    }
    public class CustomerWalletTransactionList
    {
        [Key]
        public int? TransactionId { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionMode { get; set; }
        public string CardNumber { get; set; }
        public string Note { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
