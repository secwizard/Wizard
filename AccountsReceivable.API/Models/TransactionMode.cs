using AccountsReceivable.API.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccountsReceivable.API.Models
{
    [Table("TransactionMode")]
    public class TransactionMode: IAuditableEntity
    {
        [Key]
        public int TransactionModeId { get; set; }
        public string ModeName { get; set; }
        public int? ShowInPayment { get; set; }
    }
}
