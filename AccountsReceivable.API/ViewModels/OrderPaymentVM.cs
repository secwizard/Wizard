using AccountsReceivable.API.Entities.BaseEntities;
using System;
namespace AccountsReceivable.API.ViewModels
{
    public class OrderPaymentVM: IAuditableEntity
    {
        public int OrderPaymentId { get; set; }
        public string OrderId { get; set; }
        public int? Amount { get; set; }
        public int? TransactionModeId { get; set; }
        public int? CustomerWalletId { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
