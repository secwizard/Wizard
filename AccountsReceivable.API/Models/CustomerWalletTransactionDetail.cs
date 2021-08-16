using AccountsReceivable.API.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccountsReceivable.API.Models
{
    [Table("CustomerWalletTransactionDetail")]
    public class CustomerWalletTransactionDetail: IAuditableEntity
    {
        [Key]
        public int CustomerWalletTransactionDetailId { get; set; }
        public int? CustomerWalletTransactionId { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceId { get; set; }
        public int? Amount { get; set; }
    }
}
