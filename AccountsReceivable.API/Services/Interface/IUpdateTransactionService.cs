using AccountsReceivable.API.Helpers;
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
        Task<Response<UpdateTransaction>> CustomerDepositAmount(UpdateTransaction dto);
        Task<Response<OrderPaymentRequest>> OrderWithPayment(OrderPaymentRequest dto);
        Task<Response<OrderWithOutPaymentRequest>> OrderWithOutPayment(OrderWithOutPaymentRequest dto);
    }
}
