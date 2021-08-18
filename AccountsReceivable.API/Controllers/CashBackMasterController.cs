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
    public class CashBackMasterController : Controller
    {
        private readonly ICashBackMasterService _cashBackMasterService;
        public CashBackMasterController(ICashBackMasterService cashBackMasterService)
        {
            _cashBackMasterService = cashBackMasterService;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCashBackMaster(CashBackMasterVM cashBackMasterVM)
        {
            try
            {
                return Ok(await _cashBackMasterService.AddOrUpdateCashBackMaster(cashBackMasterVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCashBackMaster()
        {
            try
            {
                return Ok(await _cashBackMasterService.GetCashBackMaster());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCashBackMasterById(int id)
        {
            try
            {
                return Ok(await _cashBackMasterService.GetCashBackMasterById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCashBackMaster(int id)
        {
            try
            {
                await _cashBackMasterService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
