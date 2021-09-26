using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.API.Models
{
    [Table("CashBackMaster")]
    public class CashBackMaster
    {
        [Key]
        public int CashBackMasterId { get; set; }
        public decimal MinimumBusinessAmount { get; set; }
        public decimal MaximumCashbackAmount { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal CashbackValue { get; set; }
        public bool IsPercentage { get; set; }
        public bool IsActive { get; set; }

    }
}
