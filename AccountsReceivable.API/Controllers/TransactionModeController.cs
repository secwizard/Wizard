using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionModeController : ControllerBase
    {
        private readonly ITransactionModeService _transactionModeService;
        public TransactionModeController(ITransactionModeService transactionModeService)
        {
            _transactionModeService = transactionModeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateTransactionMode(TransactionModeRequest transactionModeRequest)
        {
            try
            {
                return Ok(await _transactionModeService.AddUpdateTransactionMode(transactionModeRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetTransactionModes()
        {
            try
            {
                return Ok(await _transactionModeService.GetTransactionMode());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetTransactionMode(int id)
        {
            try
            {
                return Ok(await _transactionModeService.GetTransactionModeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionMode(int id)
        {
            try
            {
                await _transactionModeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
