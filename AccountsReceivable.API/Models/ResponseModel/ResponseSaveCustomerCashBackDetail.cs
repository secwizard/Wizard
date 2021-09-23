using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.ResponseModel
{
    public class ResponseSaveCustomerCashBackDetail
    {
        [Key]
        public string Message { get; set; }
    }
    public class ResponseSaveCustomerPayment
    {
        [Key]
        public int CustomerWalletId { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
    }
}
