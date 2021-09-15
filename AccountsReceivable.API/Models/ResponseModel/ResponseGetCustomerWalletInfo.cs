using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.ResponseModel
{
    public class ResponseGetCustomerWalletInfo
    {
        public CustomerWalletInfo CustomerWalletInfo { get; set; }
        public List<CustomerWalletTransactionList> CustomerWalletTransactionList { get; set; }
    }
}
