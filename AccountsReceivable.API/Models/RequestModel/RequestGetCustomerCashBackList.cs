using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models.RequestModel
{
    public class RequestGetCustomerCashBackList
    {
        public int CompanyId { get; set; }
        public List<CustomerDtl> CustomerIds { get; set; }
    }
    public class CustomerDtl
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
