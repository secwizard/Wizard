using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepositWalletAmountController : ControllerBase
    {
        private readonly ICustomerWalletService _ICustomerWalletService;
        public DepositWalletAmountController(ICustomerWalletService customerWalletService)
        {
            _ICustomerWalletService = customerWalletService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateDepositWalletAmount(CustomerWalletVM CustomerWalletVM)
        {
            try
            {
                return Ok(await _ICustomerWalletService.AddUpdateCustomerWallet(CustomerWalletVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
