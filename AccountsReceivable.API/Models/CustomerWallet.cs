using AccountsReceivable.API.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccountsReceivable.API.Models
{
    [Table("CustomerWallet")]
    public class CustomerWallet:IAuditableEntity
    {
        [Key]
        public int CustomerWalletId { get; set; }
        public int? CustomerId { get; set; }
        public int? TotalBusinessAmount { get; set; }
        public int? TotalPaidAmount { get; set; }
        public int? DueAmount { get; set; }
        public int? CreditLimit { get; set; }
    }
}
