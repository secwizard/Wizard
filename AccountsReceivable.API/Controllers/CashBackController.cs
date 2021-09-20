using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashBackController : Controller
    {
        private readonly IUpdateCashBackService _updateCashBackService;
        public CashBackController(IUpdateCashBackService updateCashBackService)
        {
            _updateCashBackService = updateCashBackService;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCashBackMaster(CashbackMasterRequest cashbackMasterRequest)
        {
            try
            {
                return Ok(await _updateCashBackService.AddCashBackForCustomer(cashbackMasterRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> GetCashbackDetails(CashbackDetail cashbackDetails)
        {
            try
            {
                return Ok(await _updateCashBackService.GetCashbackDetails(cashbackDetails));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerCashBackList(RequestGetCustomerCashBackList request)
        {
            try
            {
                return Ok(await _updateCashBackService.GetCustomerCashBackList(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomerCashBack(RequestCreateCustomerCashBack request)
        {
            try
            {
                return Ok(await _updateCashBackService.CreateCustomerCashBack(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
