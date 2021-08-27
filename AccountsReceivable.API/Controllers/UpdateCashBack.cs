using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpdateCashBackController : Controller
    {
        private readonly IUpdateCashBackService _updateCashBackService;
        public UpdateCashBackController(IUpdateCashBackService updateCashBackService)
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
    }
}
