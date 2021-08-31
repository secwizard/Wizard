using System;
namespace AccountsReceivable.API.Models.RequestModel
{
    public class CashbackMasterRequest
    {
        //public int CashBackMasterId { get; set; }
        public int? CustomerId { get; set; }
        public int? CashBackPercentage { get; set; }
        //public int? MinimumBusinessAmount { get; set; }
        //public int? MaximumCashbackAmount { get; set; }
        //public DateTime? StartDateTime { get; set; }
        //public DateTime? EndDateTime { get; set; }
        //public int? CashbackValue { get; set; }
        
        //public Boolean IsActive { get; set; }
    }
}
