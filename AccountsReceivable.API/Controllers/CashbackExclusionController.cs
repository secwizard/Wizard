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
    public class CashbackExclusionController : Controller
    {
        private readonly ICashbackExclusionService _cashbackExclusionService;
        public CashbackExclusionController(ICashbackExclusionService cashbackExclusionService)
        {
            _cashbackExclusionService = cashbackExclusionService;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCashbackExclusion(CashbackExclusionVM CashbackExclusionVM)
        {
            try
            {
                return Ok(await _cashbackExclusionService.AddOrUpdateCashbackExclusion(CashbackExclusionVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCashbackExclusion()
        {
            try
            {
                return Ok(await _cashbackExclusionService.GetCashbackExclusion());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCashbackExclusionById(int id)
        {
            try
            {
                return Ok(await _cashbackExclusionService.GetCashbackExclusionById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cashbackExclusionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
