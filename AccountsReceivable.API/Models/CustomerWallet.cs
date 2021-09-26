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
        public decimal? TotalBusinessAmount { get; set; }
        public decimal? TotalPaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? CreditLimit { get; set; }
    }
}
