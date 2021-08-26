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
    public class GetCustomerWalletInfoController : ControllerBase
    {
        private readonly IGetCustomerWalletInfoService _getCustomerWalletInfoService;
        public GetCustomerWalletInfoController(IGetCustomerWalletInfoService getCustomerWalletInfoService)
        {
            _getCustomerWalletInfoService = getCustomerWalletInfoService;
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerWalletInfo(int customerid)
        {
            try
            {
                return Ok(await _getCustomerWalletInfoService.GetCustomerWalletInfo(customerid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
