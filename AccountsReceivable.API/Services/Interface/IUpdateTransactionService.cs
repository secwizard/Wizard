using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models.RequestModel;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IUpdateTransactionService
    {
        Task<Response<UpdateTransaction>> CustomerDepositAmount(UpdateTransaction dto);
        Task<Response<OrderPaymentRequest>> OrderWithPayment(OrderPaymentRequest dto);
        Task<Response<OrderWithOutPaymentRequest>> OrderWithOutPayment(OrderWithOutPaymentRequest dto);
    }
}
