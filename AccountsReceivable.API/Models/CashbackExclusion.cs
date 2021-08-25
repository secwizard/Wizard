using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models
{
    [Table("CashbackExclusion")]
    public class CashbackExclusion
    {
        [Key]
        public int CashbackExclusionId { get; set; }
        public int? CashBackMasterId { get; set; }
        public int? CustomerId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
