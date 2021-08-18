using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.RequestModel
{
    public class CustomerWalletRequest
    {
        public int? CustomerId { get; set; }
        public int? CreditLimit { get; set; }
    }
}
