using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerWalletController : ControllerBase
    {
        private readonly ICustomerWalletService _cwService;
        public CustomerWalletController(ICustomerWalletService cwService)
        {
            _cwService = cwService;
        }

        [HttpPost]
        public async Task<Response<UpdateTransaction>> CreateNewCustomerWallet(UpdateTransaction updateTransaction)
        {
            return await _cwService.CreateNewCustomerWallet(updateTransaction);
        }

        [HttpPost()]
        public async Task<IActionResult> GetCustomerWalletInfo(int customerid)
        {
            try
            {
                return Ok(await _cwService.GetCustomerWalletInfo(customerid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CheckCustomerWalletDetailForPlaceOrder(CheckCustomerWalletDetailForPlaceOrder request)
        {
            try
            {
                return Ok(await _cwService.CheckCustomerWalletDetailForPlaceOrder(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCustomerPayment(SaveCustomerPaymentRequest saveCustomerPaymentRequest)
        {
            try
            {
                return Ok(await _cwService.SaveCustomerPayment(saveCustomerPaymentRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
