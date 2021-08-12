using AccountsReceivable.API.Data;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services
{
    public class OrderPaymentService : IOrderPaymentService
    {
        #region Declarations
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public OrderPaymentService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion
        public async Task<OrderPaymentVM> AddUpdateOrderPayment(OrderPaymentVM dto)
        {
            if (dto != null)
            {
                OrderPayment orderPayment = await _context.OrderPayment.FirstOrDefaultAsync(x => x.OrderPaymentId == dto.OrderPaymentId);
                if (orderPayment == null)
                {
                    orderPayment = new OrderPayment();
                    OrderPayment orderPaymentData = _mapper.Map<OrderPaymentVM, OrderPayment>(dto);
                    _context.OrderPayment.Add(orderPaymentData);
                }
                else
                {
                    _context.Entry(orderPayment).CurrentValues.SetValues(dto);
                }
                await _context.SaveChangesAsync();
                return dto;
            }
            return dto;
        }
        public async Task Delete(int id)
        {
            if (id > 0)
            {
                OrderPayment orderPayment = _context.OrderPayment.Find(id);
                if (orderPayment != null)
                {
                    _context.OrderPayment.Remove(orderPayment);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<OrderPaymentVM> GetOrderPaymentById(int id)
        {
            OrderPayment orderPayment = await _context
               .OrderPayment
               .SingleOrDefaultAsync(x => x.OrderPaymentId == id);
            return _mapper.Map<OrderPayment, OrderPaymentVM>(orderPayment);
        }
        public async Task<List<OrderPaymentVM>> GetOrderPayment()
        {
            List<OrderPayment> orderPayments = await _context.OrderPayment.ToListAsync();

            List<OrderPaymentVM> data = _mapper.Map<List<OrderPayment>, List<OrderPaymentVM>>(orderPayments);

            return data;
        }
    }
}
