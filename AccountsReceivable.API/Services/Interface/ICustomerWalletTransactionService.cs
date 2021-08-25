using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface ICustomerWalletTransactionService
    {
        Task<List<CustomerWalletTransactionVM>> GetCustomerWalletTransactions();
        Task<CustomerWalletTransactionVM> GetCustomerWalletTransactionById(int id);
  //      Task<CustomerWalletTransactionVM> AddUpdateCustomerWalletTransaction(CustomerWalletTransactionVM dto);
        Task Delete(int id);
    }
}
