using System;
namespace AccountsReceivable.API.Models.RequestModel
{
    public class CashbackMasterRequest
    {
        //public int CashBackMasterId { get; set; }
        public int? CompanyId { get; set; }
        public int? MinimumBusinessAmount { get; set; }
        public int? MaximumCashbackAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CashbackValue { get; set; }
        public int? IsPercentage { get; set; }
        public Boolean IsActive { get; set; }

        //public DateTime? StartDateTime { get; set; }
        //public DateTime? EndDateTime { get; set; }

    }
}
