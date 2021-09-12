using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.RequestModel
{
    public class CheckCustomerWalletDetailForPlaceOrder
    {
        public int CustomerId { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
