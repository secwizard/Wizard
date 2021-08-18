using AccountsReceivable.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface ICashBackMasterService
    {
        Task<List<CashBackMasterVM>> GetCashBackMaster();
        Task<CashBackMasterVM> GetCashBackMasterById(int id);
        Task<CashBackMasterVM> AddOrUpdateCashBackMaster(CashBackMasterVM dto);
        Task Delete(int id);
    }
}
