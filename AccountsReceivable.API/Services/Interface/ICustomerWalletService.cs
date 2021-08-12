using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface ICustomerWalletService
    {
        Task<List<CustomerWalletVM>> GetCustomerWallet();
        Task<CustomerWalletVM> GetCustomerWalletById(int id);
        Task<CustomerWalletVM> AddUpdateCustomerWallet(CustomerWalletVM dto);
        Task Delete(int id);
    }
}
