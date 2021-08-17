using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface ITransactionModeService
    {
        Task<List<TransactionModeVM>> GetTransactionMode();
        Task<TransactionModeVM> GetTransactionModeById(int id);
   //     Task<TransactionModeVM> AddUpdateTransactionMode(TransactionModeVM dto);
        Task Delete(int id);
    }
}
