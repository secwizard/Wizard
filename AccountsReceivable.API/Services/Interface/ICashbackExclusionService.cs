using AccountsReceivable.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface ICashbackExclusionService
    {
        Task<List<CashbackExclusionVM>> GetCashbackExclusion();
        Task<CashbackExclusionVM> GetCashbackExclusionById(int id);
        Task<CashbackExclusionVM> AddOrUpdateCashbackExclusion(CashbackExclusionVM dto);
        Task Delete(int id);
    }
}
