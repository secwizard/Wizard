using AccountsReceivable.API.Entities.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.API.Models
{
    [Table("CustomerWalletTransaction")]
    public class CustomerWalletTransaction : IAuditableEntity
    {
        [Key]
        public int CustomerWalletTransactionId { get; set; }
        public int? CustomerWalletId { get; set; }
        public int? CustomerId { get; set; }

        public int? TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public int? TransactionModeId { get; set; }
        public DateTime? Date { get; set; }
        public string? CreditCard { get; set; } = null!;
    }
}
