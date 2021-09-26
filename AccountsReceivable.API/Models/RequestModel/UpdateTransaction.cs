namespace AccountsReceivable.API.Models.RequestModel
{
    public class UpdateTransaction
    {
        public int? CustomerId { get; set; }
        public decimal? Amount { get; set; }
        public int UserId { get; set; }
    }
}
