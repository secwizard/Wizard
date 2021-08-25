using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.RequestModel
{
    public class UpdateTransaction
    {
        public int? CustomerId { get; set; }
        public int? Amount { get; set; }
    }
}
