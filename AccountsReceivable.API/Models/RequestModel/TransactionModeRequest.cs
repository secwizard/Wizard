using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.RequestModel
{
    public class TransactionModeRequest
    {
        public string ModeName { get; set; }
        public int? ShowInPayment { get; set; }
    }
}
