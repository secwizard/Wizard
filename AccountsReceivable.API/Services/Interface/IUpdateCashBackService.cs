using AccountsReceivable.API.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IUpdateCashBackService
    {
        Task<CashbackMasterRequest> AddCashBackForCustomer(CashbackMasterRequest dto);
    }
}
