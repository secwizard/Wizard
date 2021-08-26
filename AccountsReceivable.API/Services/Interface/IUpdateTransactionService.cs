using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IUpdateTransactionService
    {
        Task<UpdateTransaction> CustomerDepositAmount(UpdateTransaction dto);
        Task<OrderPaymentRequest> OrderWithPayment(OrderPaymentRequest dto);
        Task<OrderWithOutPaymentRequest> OrderWithOutPayment(OrderWithOutPaymentRequest dto);
    }
}
