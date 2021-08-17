using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomerWalletController : ControllerBase
    {
        private readonly ICustomerWalletService _customerWalletService;
        public CustomerWalletController(ICustomerWalletService customerWalletService)
        {
            _customerWalletService = customerWalletService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCustomerWallet(CustomerWalletRequest customerWalletVM)
        {
            try
            {
                return Ok(await _customerWalletService.AddUpdateCustomerWallet(customerWalletVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerWallets()
        {
            try
            {
                return Ok(await _customerWalletService.GetCustomerWallet());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerWallet(int id)
        {
            try
            {
                return Ok(await _customerWalletService.GetCustomerWalletById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerWallet(int id)
        {
            try
            {
                await _customerWalletService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
