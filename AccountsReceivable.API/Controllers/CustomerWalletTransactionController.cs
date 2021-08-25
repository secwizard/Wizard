using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerWalletTransactionController : Controller
    {
        private readonly ICustomerWalletTransactionService _customerWalletTransactionService;

        public CustomerWalletTransactionController(ICustomerWalletTransactionService customerWalletTransactionService)
        {
            _customerWalletTransactionService = customerWalletTransactionService;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddOrUpdateCustomerWalletTransaction(CustomerWalletTransactionVM customerWalletTransactionVM)
        //{
        //    try
        //    {
        //        return Ok(await _customerWalletTransactionService.AddUpdateCustomerWalletTransaction(customerWalletTransactionVM));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GetCustomerWalletTransactions()
        {
            try
            {
                return Ok(await _customerWalletTransactionService.GetCustomerWalletTransactions());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerWalletTransaction(int id)
        {
            try
            {
                return Ok(await _customerWalletTransactionService.GetCustomerWalletTransactionById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerWalletTransaction(int id)
        {
            try
            {
                await _customerWalletTransactionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
