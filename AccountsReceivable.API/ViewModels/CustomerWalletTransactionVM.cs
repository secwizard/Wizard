using AccountsReceivable.API.Entities.BaseEntities;
using System;

namespace AccountsReceivable.API.ViewModels
{
    public class CustomerWalletTransactionVM : IAuditableEntity
    {
        public int CustomerWalletTransactionId { get; set; }
        public int? CustomerWalletId { get; set; }
        public int? CustomerId { get; set; }
        public int? TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public int? TransactionModeId { get; set; }
        public DateTime? Date { get; set; }
        public string CreditCard { get; set; }
    }
}
