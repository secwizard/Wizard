using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.ViewModels
{
    public class CashbackExclusionVM
    {
        public int CashbackExclusionId { get; set; }
        public int? CashBackMasterId { get; set; }
        public int? CustomerId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
