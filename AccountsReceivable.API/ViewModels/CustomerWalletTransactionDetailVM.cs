using AccountsReceivable.API.Entities.BaseEntities;
namespace AccountsReceivable.API.ViewModels
{
    public class CustomerWalletTransactionDetailVM: IAuditableEntity
    {
        public int CustomerWalletTransactionDetailId { get; set; }
        public int? CustomerWalletTransactionId { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceId { get; set; }
        public int? Amount { get; set; }
    }
}
