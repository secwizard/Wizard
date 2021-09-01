using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpdateTransactionController : Controller
    {
        private readonly IUpdateTransactionService _updateTransactionService;
        public UpdateTransactionController(IUpdateTransactionService updateTransactionService)
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
    }
}
