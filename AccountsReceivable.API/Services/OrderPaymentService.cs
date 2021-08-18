using AccountsReceivable.API.Data;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
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
        public async Task<OrderPaymentRequest> AddUpdateOrderPayment(OrderPaymentRequest dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (dto != null)
                    {
                        if (dto.CustomerId != null)
                        {
                            var OrderpaymentDataInserted = false;
                            OrderPayment orderPayment = await _context.OrderPayment.FirstOrDefaultAsync(x => x.OrderId == dto.OrderId || x.CustomerWalletId == dto.CustomerWalletId);
                            if (orderPayment == null)
                            {
                                OrderpaymentDataInserted = true;
                                orderPayment = new OrderPayment();
                                OrderPayment orderPaymentData = _mapper.Map<OrderPaymentRequest, OrderPayment>(dto);
                                orderPayment.CreatedDate = DateTime.UtcNow;
                                orderPayment.CreatedBy = dto.CustomerId;
                                _context.OrderPayment.Add(orderPaymentData);
                            }
                            else
                            {
                                orderPayment.ModifiedDate = DateTime.UtcNow;
                                orderPayment.ModifiedBy = dto.CustomerId;
                                _context.Entry(orderPayment).CurrentValues.SetValues(dto);
                            }
                            int id = await _context.SaveChangesAsync();
                            if (id > 0 && OrderpaymentDataInserted && dto.TransactionModeId != null && dto.TransactionModeId <= 3)
                            {
                                CustomerWalletTransactionDetail CustTranDetails = new CustomerWalletTransactionDetail();
                                if (dto.TransactionModeId == 0)
                                {
                                    CustomerWalletTransaction customerWalletTransaction = new CustomerWalletTransaction();
                                    customerWalletTransaction.CustomerWalletId = dto.CustomerWalletId;
                                    customerWalletTransaction.TransactionAmount = dto.Amount;
                                    customerWalletTransaction.CreatedDate = DateTime.UtcNow;
                                    customerWalletTransaction.CreatedBy = dto.CustomerId;
                                    customerWalletTransaction.TransactionType = "Order";
                                    customerWalletTransaction.TransactionModeId = dto.TransactionModeId;
                                    _context.CustomerWalletTransaction.Add(customerWalletTransaction);
                                    CustTranDetails.ReferenceId = dto.OrderId;
                                    CustTranDetails.Amount = dto.Amount;
                                    CustTranDetails.CreatedDate = DateTime.UtcNow;
                                    CustTranDetails.CreatedBy = dto.CustomerId;
                                    _context.CustomerWalletTransactionDetail.Add(CustTranDetails);
                                }
                                else
                                {
                                    CustomerWalletTransaction customerWalletTransaction = new CustomerWalletTransaction();
                                    customerWalletTransaction.CustomerWalletId = dto.CustomerWalletId;
                                    customerWalletTransaction.TransactionAmount = dto.Amount;
                                    customerWalletTransaction.CreatedDate = DateTime.UtcNow;
                                    customerWalletTransaction.CustomerId = dto.CustomerId;
                                    customerWalletTransaction.TransactionType = "OrderPayment";
                                    customerWalletTransaction.TransactionModeId = dto.TransactionModeId;
                                    _context.CustomerWalletTransaction.Add(customerWalletTransaction);
                                    CustTranDetails.ReferenceId = dto.OrderId;
                                    CustTranDetails.Amount = Convert.ToInt32("-" + dto.Amount);
                                    CustTranDetails.CreatedDate = DateTime.UtcNow;
                                    CustTranDetails.CreatedBy = dto.CustomerId;
                                    _context.CustomerWalletTransactionDetail.Add(CustTranDetails);
                                }
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();//Execute when both tables data will inserted.
                            }
                            else
                            {
                                transaction.Rollback();
                                throw new Exception("Invalid request.");
                            }
                            return dto;
                        }
                        else
                        {
                            transaction.Rollback();
                            throw new Exception("Please Select CustomerID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return dto;
            }
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
