using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface ICustomerWalletService
    {
        Task<List<CustomerWalletVM>> GetCustomerWallet();
        Task<CustomerWalletVM> GetCustomerWalletById(int id);
        Task<CustomerWalletRequest> AddUpdateCustomerWallet(CustomerWalletRequest dto);
        Task Delete(int id);
    }
}
