using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderPaymentController : ControllerBase
    {
        private readonly IOrderPaymentService _orderPaymentService;
        public OrderPaymentController(IOrderPaymentService orderPaymentService)
        {
            _orderPaymentService = orderPaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateOrderPayment(OrderPaymentVM orderPaymentVM)
        {
            try
            {
                return Ok(await _orderPaymentService.AddUpdateOrderPayment(orderPaymentVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderPaymentDetails()
        {
            try
            {
                return Ok(await _orderPaymentService.GetOrderPayment());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderPaymentDetail(int id)
        {
            try
            {
                return Ok(await _orderPaymentService.GetOrderPaymentById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderPaymentDetail(int id)
        {
            try
            {
                await _orderPaymentService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
