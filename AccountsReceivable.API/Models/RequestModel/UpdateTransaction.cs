namespace AccountsReceivable.API.Models.RequestModel
{
    public class UpdateTransaction
    {
        public int? CustomerId { get; set; }
        public int? Amount { get; set; }
        public string TransactionMode { get; set; }
        public string CardNumber { get; set; }
    }
}
