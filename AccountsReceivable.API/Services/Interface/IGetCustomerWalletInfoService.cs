using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface IGetCustomerWalletInfoService
    {
        Task<Response<ResponseGetCustomerWalletInfo>> GetCustomerWalletInfo(int customerId);
    }
}
