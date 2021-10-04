using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Models.ResponseModel;
using AccountsReceivable.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly IUpdateTransactionService _updateTransactionService;
        public TransactionController(IUpdateTransactionService updateTransactionService)
        {
            _updateTransactionService = updateTransactionService;
        }
        
        [HttpPost]
        public async Task<Response<UpdateTransaction>> CustomerDepositAmount(UpdateTransaction updateTransaction)
        {
            return await _updateTransactionService.CustomerDepositAmount(updateTransaction);
        }
        
        [HttpPost]
        public async Task<IActionResult> OrderWithPayment(OrderPaymentRequest orderPaymentVM)
        {
            try
            {
                return Ok(await _updateTransactionService.OrderWithPayment(orderPaymentVM));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> OrderWithOutPayment(OrderWithOutPaymentRequest orderPaymentVM)
        {
            try
            {
                return Ok(await _updateTransactionService.OrderWithOutPayment(orderPaymentVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseList<ResponseCustomerOrderPaymentList>> CustomerOrderPaymentList(CustomerOrderPaymentList request)
        {
            return await _updateTransactionService.CustomerOrderPaymentList(request);
        }

        [HttpPost]
        public async Task<ResponseList<ResponseGetTransactionMode>> GetTransactionMode()
        {
            return await _updateTransactionService.GetTransactionMode();
        }
    }
}
