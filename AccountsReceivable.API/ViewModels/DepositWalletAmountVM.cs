using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.ViewModels
{
    public class DepositWalletAmountVM
    {
        public int CustomerWalletId { get; set; }
        public int? CustomerId { get; set; }
        public int? CreditLimit { get; set; }
    }
}
