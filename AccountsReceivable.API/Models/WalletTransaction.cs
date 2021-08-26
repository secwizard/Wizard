using System;
namespace AccountsReceivable.API.Models
{
    public class WalletTransaction
    {
        public int? CustomerWalletId { get; set; }
        public int? TransactionAmount { get; set; }
        public String TransactionType { get; set; }
        public string OrderId { get; set; }
        public string TransactionMode { get; set; }
        public DateTime? TransactionDate { get; set; }
        //public int? IsPercentage { get; set; }
        //public Boolean IsActive { get; set; }
    }
}
