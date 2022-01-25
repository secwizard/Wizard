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

    public class SaveCustomerPaymentRequest
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public int TransactionModeId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeHolderName { get; set; }
        public string Note { get; set; }
        public string CreatedFrom { get; set; }
        public string OrderDetails { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
