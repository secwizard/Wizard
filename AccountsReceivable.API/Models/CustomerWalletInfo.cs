using System;
namespace AccountsReceivable.API.Models
{
    public class CustomerWalletInfo
    {
        public int? TransactionAmount { get; set; }
        public int? TotalBusinessAmount { get; set; }
        public int? TotalPaidAmount { get; set; }
        public int? DueAmount { get; set; }
        public string TransactionType { get; set; }
        public int? TransactionModeId { get; set; }
        public string CardNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string OrderId { get; set; }
        //public int? IsPercentage { get; set; }
        //public Boolean IsActive { get; set; }
    }
}
