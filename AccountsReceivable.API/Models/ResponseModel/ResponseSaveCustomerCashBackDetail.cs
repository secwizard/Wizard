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
}
