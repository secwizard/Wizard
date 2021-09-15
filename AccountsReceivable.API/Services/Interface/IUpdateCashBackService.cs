using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IUpdateCashBackService
    {
        Task<Response<CashbackMasterRequest>> AddCashBackForCustomer(CashbackMasterRequest dto);
        Task<Response<CashbackDetail>> GetCashbackDetails(CashbackDetail dto); 
        Task<ResponseList<GetCustomerCashBackList>> GetCustomerCashBackList(RequestGetCustomerCashBackList dto); 
    }
}
