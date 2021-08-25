﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.ViewModels
{
    public class CashBackMasterVM
    {
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
