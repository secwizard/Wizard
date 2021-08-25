using AccountsReceivable.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface ICashBackTransactionService
    {
        Task<List<CashBackTransactionVM>> GetCashBackTransaction();
        Task<CashBackTransactionVM> GetCashBackTransactionById(int id);
        Task<CashBackTransactionVM> AddOrUpdateCashBackTransaction(CashBackTransactionVM dto);
        Task Delete(int id);
    }
}
