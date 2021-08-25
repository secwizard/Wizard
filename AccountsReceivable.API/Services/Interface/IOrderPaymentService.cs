using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IOrderPaymentService
    {
        Task<List<OrderPaymentVM>> GetOrderPayment();
        Task<OrderPaymentVM> GetOrderPaymentById(int id);
        Task<OrderPaymentRequest> AddUpdateOrderPayment(OrderPaymentRequest dto);
        Task Delete(int id);
    }
}
