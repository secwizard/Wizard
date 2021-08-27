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
    public class UpdateTransactionService : IUpdateTransactionService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        public UpdateTransactionService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UpdateTransaction> CustomerDepositAmount(UpdateTransaction dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (dto != null)
                    {
                        CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);
                        // bool isNewCustomerWallet = false;
                        CustomerWallet customerWalletData = _mapper.Map<UpdateTransaction, CustomerWallet>(dto);
                        if (customerWallet != null)
                        {
                            customerWallet.ModifiedDate = DateTime.UtcNow;
                            customerWallet.ModifiedBy = dto.CustomerId;
                            customerWallet.CreditLimit = dto.Amount;
                            _context.Entry(customerWallet).CurrentValues.SetValues(dto);
                            CustomerWalletTransaction CWalletTransaction = new CustomerWalletTransaction();
                            CWalletTransaction.ModifiedDate = DateTime.UtcNow;
                            CWalletTransaction.ModifiedBy = dto.CustomerId;
                            CWalletTransaction.TransactionAmount = dto.Amount;
                            CWalletTransaction.TransactionType = "Deposit";
                            CWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                            _context.CustomerWalletTransaction.Add(CWalletTransaction);
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync(); //Execute when both tables data will inserted.
                            return dto;
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
        public async Task<OrderPaymentRequest> OrderWithPayment(OrderPaymentRequest dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (dto != null)
                    {
                        CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);
                        OrderPayment orderPayment = new OrderPayment();
                        if (customerWallet != null)
                        {
                            if (dto.TransactionMode.ToLower() == "creditCard")
                            {
                                orderPayment.TransactionModeId = 0;
                               // orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                            }
                            else if (dto.TransactionMode.ToLower() == "debitCard")
                            {
                                orderPayment.TransactionModeId = 1;
                            //    orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                            }
                            else if (dto.TransactionMode.ToLower() == "cheque")
                            {
                                orderPayment.TransactionModeId = 2;
                              //  orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                            }
                            else if (dto.TransactionMode.ToLower() == "cash")
                            {
                                orderPayment.TransactionModeId = 3;
                                //orderPayment.TransactionModeNumber = "";
                            }
                            else
                            {
                                transaction.Rollback();
                                throw new Exception("Customer is not select Transaction Mode.");
                            }
                            // OrderpaymentDataInserted = true;
                            // orderPayment = new OrderPayment();
                            OrderPayment orderPaymentData = _mapper.Map<OrderPaymentRequest, OrderPayment>(dto);
                            orderPaymentData.CustomerId = dto.CustomerId;
                            orderPaymentData.CreatedBy = dto.CustomerId;
                            orderPaymentData.CreatedDate = DateTime.UtcNow;
                            orderPaymentData.TransactionModeId = orderPayment.TransactionModeId;
                            _context.OrderPayment.Add(orderPaymentData);
                            // _context.Entry(orderPayment).CurrentValues.SetValues(dto);
                            int id = await _context.SaveChangesAsync();
                            if (id > 0 && dto.TransactionMode != null)
                            {
                                CustomerWalletTransactionDetail CustTranDetails = new CustomerWalletTransactionDetail();
                                CustomerWalletTransaction customerWalletTransaction = new CustomerWalletTransaction();
                                customerWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                                customerWalletTransaction.TransactionAmount = dto.Amount;
                                customerWalletTransaction.CreatedDate = DateTime.UtcNow;
                                customerWalletTransaction.CreatedBy = dto.CustomerId;
                                customerWalletTransaction.CardNumber = dto.transactionModeNumber;
                                customerWalletTransaction.TransactionModeId = orderPayment.TransactionModeId;
                                customerWalletTransaction.CustomerId = dto.CustomerId;
                                customerWalletTransaction.TransactionType = "OrderPayment";
                                _context.CustomerWalletTransaction.Add(customerWalletTransaction);
                                CustTranDetails.ReferenceId = dto.OrderId;
                                CustTranDetails.Amount = Convert.ToInt32("-" + dto.Amount);
                                CustTranDetails.CreatedDate = DateTime.UtcNow;
                                CustTranDetails.CustomerId = dto.CustomerId;
                                CustTranDetails.CreatedBy = dto.CustomerId;
                                CustTranDetails.CustomerWalletTransactionId = customerWalletTransaction.CustomerWalletTransactionId;
                                _context.CustomerWalletTransactionDetail.Add(CustTranDetails);
                            }

                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();//Execute when both tables data will inserted.
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                        throw new Exception("Customer is not exists.");
                    }
                    return dto;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return dto;
            }
        }
        public async Task<OrderWithOutPaymentRequest> OrderWithOutPayment(OrderWithOutPaymentRequest dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (dto != null)
                    {
                        // var OrderpaymentDataInserted = false;
                        CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);

                        OrderPayment orderPayment = new OrderPayment();
                        if (customerWallet != null)
                        {
                           // OrderpaymentDataInserted = true;
                            //orderPayment = new OrderPayment();
                            OrderPayment orderPaymentData = _mapper.Map<OrderWithOutPaymentRequest, OrderPayment>(dto);
                            orderPaymentData.CreatedBy = dto.CustomerId;
                            orderPaymentData.CreatedDate = DateTime.UtcNow;
                            _context.OrderPayment.Add(orderPaymentData);
                            int id = await _context.SaveChangesAsync();
                            if (id > 0 && dto.CustomerId != null)
                            {
                                CustomerWalletTransactionDetail CustTranDetails = new CustomerWalletTransactionDetail();
                                CustomerWalletTransaction customerWalletTransaction = new CustomerWalletTransaction();
                                customerWalletTransaction.CustomerWalletId = orderPayment.CustomerWalletId;
                                customerWalletTransaction.TransactionAmount = dto.Amount;
                                customerWalletTransaction.CreatedDate = DateTime.UtcNow;
                                customerWalletTransaction.CreatedBy = dto.CustomerId;
                                customerWalletTransaction.TransactionType = "Order";
                                _context.CustomerWalletTransaction.Add(customerWalletTransaction);
                                CustTranDetails.ReferenceId = dto.OrderId;
                                CustTranDetails.Amount = dto.Amount;
                                CustTranDetails.CreatedDate = DateTime.UtcNow;
                                CustTranDetails.CreatedBy = dto.CustomerId;
                                _context.CustomerWalletTransactionDetail.Add(CustTranDetails);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();//Execute when both tables data will inserted.
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            throw new Exception("Customer is not exists.");
                        }
                        return dto;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return dto;
            }
        }
    }
}
