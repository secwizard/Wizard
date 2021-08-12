using AccountsReceivable.API.Entities.BaseEntities;
namespace AccountsReceivable.API.ViewModels
{
    public class TransactionModeVM: IAuditableEntity
    {
        public int TransactionModeId { get; set; }
        public string ModeName { get; set; }
        public int? ShowInPayment { get; set; }
    }
}
