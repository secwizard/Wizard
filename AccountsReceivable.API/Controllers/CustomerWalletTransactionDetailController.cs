using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerWalletTransactionDetailController : ControllerBase
    {
        private readonly ICustomerWalletTransactionDetailService _customerWalletTransactionDetailService;
        public CustomerWalletTransactionDetailController(ICustomerWalletTransactionDetailService customerWalletTransactionDetailService)
        {
            _customerWalletTransactionDetailService = customerWalletTransactionDetailService;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddOrUpdateCustomerWalletTransactionDetail(CustomerWalletTransactionDetailVM customerWalletTransactionVM)
        //{
        //    try
        //    {
        //        return Ok(await _customerWalletTransactionDetailService.AddUpdateCustomerWalletTransactionDetail(customerWalletTransactionVM));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GetCustomerWalletTransactionDetails()
        {
            try
            {
                return Ok(await _customerWalletTransactionDetailService.GetCustomerWalletTransactionDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerWalletTransactionDetail(int id)
        {
            try
            {
                return Ok(await _customerWalletTransactionDetailService.GetCustomerWalletTransactionDetailById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerWalletTransactionDetail(int id)
        {
            try
            {
                await _customerWalletTransactionDetailService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
