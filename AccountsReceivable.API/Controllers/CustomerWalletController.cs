using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerWalletController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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
                log.Info("***GetCustomerWalletInfo*** Date : " + DateTime.UtcNow + " Error : " + ex.Message + "StackTrace : " + ex.StackTrace.ToString());
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
                log.Info("***CheckCustomerWalletDetailForPlaceOrder*** Date : " + DateTime.UtcNow + " Error : " + ex.Message + "StackTrace : " + ex.StackTrace.ToString());
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
                log.Info("***SaveCustomerPayment*** Date : " + DateTime.UtcNow + " Error : " + ex.Message + "StackTrace : " + ex.StackTrace.ToString());
                return BadRequest(ex);
            }
        }
    }
}
