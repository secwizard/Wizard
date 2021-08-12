using AccountsReceivable.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IOrderPaymentService
    {
        Task<List<OrderPaymentVM>> GetOrderPayment();
        Task<OrderPaymentVM> GetOrderPaymentById(int id);
        Task<OrderPaymentVM> AddUpdateOrderPayment(OrderPaymentVM dto);
        Task Delete(int id);
    }
}
