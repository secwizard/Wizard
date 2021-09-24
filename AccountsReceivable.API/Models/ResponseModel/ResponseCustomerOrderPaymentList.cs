using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.ResponseModel
{
    public class ResponseCustomerOrderPaymentList
    {
        [Key]
        public int OrderId { get; set; }
        public int Amount { get; set; }
    }
}
