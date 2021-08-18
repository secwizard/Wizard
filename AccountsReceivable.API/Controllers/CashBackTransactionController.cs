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
    public class CashBackTransactionController : Controller
    {
        private readonly ICashBackTransactionService _cashBackTransactionService;
        public CashBackTransactionController(ICashBackTransactionService cashBackTransactionService)
        {
            _cashBackTransactionService = cashBackTransactionService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateCashBackTransaction(CashBackTransactionVM cashBackTransactionVM)
        {
            try
            {
                return Ok(await _cashBackTransactionService.AddOrUpdateCashBackTransaction(cashBackTransactionVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCashBackTransaction()
        {
            try
            {
                return Ok(await _cashBackTransactionService.GetCashBackTransaction());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCashBackTransactionById(int id)
        {
            try
            {
                return Ok(await _cashBackTransactionService.GetCashBackTransactionById(id));
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
                await _cashBackTransactionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
