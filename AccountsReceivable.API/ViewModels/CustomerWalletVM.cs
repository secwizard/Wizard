using AccountsReceivable.API.Entities.BaseEntities;
using System;

namespace AccountsReceivable.API.ViewModels
{
    public class CustomerWalletVM: IAuditableEntity
    {
        public int CustomerWalletId { get; set; }
        public int? CustomerId { get; set; }
        public int? TotalBusinessAmount { get; set; }
        public int? TotalPaidAmount { get; set; }
        public int? DueAmount { get; set; }
        public int? CreditLimit { get; set; }
    }
}
