using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface ICustomerWalletTransactionDetailService
    {
        Task<List<CustomerWalletTransactionDetailVM>> GetCustomerWalletTransactionDetails();
        Task<CustomerWalletTransactionDetailVM> GetCustomerWalletTransactionDetailById(int id);
   //     Task<CustomerWalletTransactionDetailVM> AddUpdateCustomerWalletTransactionDetail(CustomerWalletTransactionDetailVM dto);
        Task Delete(int id);
    }
}
