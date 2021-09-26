using System.ComponentModel.DataAnnotations;

namespace AccountsReceivable.API.Models
{
    public class DepositWalletAmount
    {
        [Key]
        public int CustomerWalletId { get; set; }
        public int? CustomerId { get; set; }
        //public int? TotalBusinessAmount { get; set; }
        //public int? TotalPaidAmount { get; set; }
        //public int? DueAmount { get; set; }
        public decimal? CreditLimit { get; set; }
    }
}
