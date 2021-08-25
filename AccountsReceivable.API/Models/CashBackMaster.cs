using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.API.Models
{
    [Table("CashBackMasters")]
    public class CashBackMasters
    {
        [Key]
        public int? CustomerId { get; set; }
        public int? MinimumBusinessAmount { get; set; }
        public int? MaximumCashbackAmount { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? CashbackValue { get; set; }
        public int? IsPercentage { get; set; }
        public Boolean IsActive { get; set; }
    }
}
