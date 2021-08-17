using AccountsReceivable.API.Entities.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccountsReceivable.API.Models
{
    [Table("OrderPayment")]
    public class OrderPayment: IAuditableEntity
    {
        [Key]
        public int OrderPaymentId { get; set; }
        public string OrderId { get; set; }
        public int? Amount { get; set; }
        public int? CustomerId { get; set; }
        public int? TransactionModeId { get; set; }
        public int? CustomerWalletId { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
