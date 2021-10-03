using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.ResponseModel
{
    public class ResponseGetTransactionMode
    {
        [Key]
        public int TransactionModeId { get; set; }
        public string ModeName { get; set; }
    }
}
