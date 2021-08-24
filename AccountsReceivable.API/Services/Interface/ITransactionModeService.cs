using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface ITransactionModeService
    {
        Task<List<TransactionModeVM>> GetTransactionMode();
        Task<TransactionModeVM> GetTransactionModeById(int id);
        Task<TransactionModeRequest> AddUpdateTransactionMode(TransactionModeRequest dto);
        Task Delete(int id);
    }
}
