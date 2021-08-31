using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface IGetCustomerWalletInfoService
    {
        Task<Response<List<CustomerWalletInfo>>> GetCustomerWalletInfo(int customerId);
    }
}
