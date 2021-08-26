using AccountsReceivable.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Services.Interface
{
    public interface IGetCustomerWalletInfoService
    {
        Task<List<CustomerWalletInfo>> GetCustomerWalletInfo(int customerId);
    }
}
