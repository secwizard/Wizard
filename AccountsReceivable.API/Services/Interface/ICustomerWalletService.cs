using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Models.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface ICustomerWalletService
    {
        Task<Response<ResponseGetCustomerWalletInfo>> GetCustomerWalletInfo(int customerId);
        Task<Response<UpdateTransaction>> CreateNewCustomerWallet(UpdateTransaction updateTransaction);
        Task<Response<ResponseCheckCustomerWalletDetailForPlaceOrder>> CheckCustomerWalletDetailForPlaceOrder(CheckCustomerWalletDetailForPlaceOrder request); 
    }
}
