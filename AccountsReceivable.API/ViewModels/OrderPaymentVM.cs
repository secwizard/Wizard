using AccountsReceivable.API.Entities.BaseEntities;
using System;
namespace AccountsReceivable.API.ViewModels
{
    public class OrderPaymentVM: IAuditableEntity
    {
        public int OrderPaymentId { get; set; }
        public string OrderId { get; set; }
        public int? Amount { get; set; }
        public int? TransactionMethod { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
