using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.RequestModel
{
    public class OrderPaymentRequest
    {
        public string OrderId { get; set; }
        public int? OrderAmount { get; set; }
        public string TransactionMode { get; set; }
        public string transactionModeNumber { get; set; }
        public int UserId { get; set; }
        public int? CustomerId { get; set; }
    }
}
