using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<Response<UpdateTransaction>> CustomerDepositAmount(UpdateTransaction dto)
        {
            Response<UpdateTransaction> responseobj = new Response<UpdateTransaction>();

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
                            customerWallet.ModifiedDate = DateTime.Now;
                            customerWallet.ModifiedBy = dto.CustomerId;
                            customerWallet.CreditLimit = dto.Amount;
                            _context.Entry(customerWallet).CurrentValues.SetValues(dto);
                            CustomerWalletTransaction CWalletTransaction = new CustomerWalletTransaction();
                            CWalletTransaction.ModifiedDate = DateTime.Now;
                            CWalletTransaction.ModifiedBy = dto.CustomerId;
                            CWalletTransaction.TransactionAmount = dto.Amount;
                            CWalletTransaction.TransactionType = "Deposit";
                            CWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                            _context.CustomerWalletTransaction.Add(CWalletTransaction);
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync(); //Execute when both tables data will inserted.
                            responseobj.Data = dto;
                            responseobj.Status.Code = (int)HttpStatusCode.OK;
                            responseobj.Status.Message = "Customer Wallet updated succesfully.";
                            responseobj.Status.Response = "Success";
                        }
                        else
                        {
                            customerWallet = new CustomerWallet();
                            customerWallet.CreatedDate = DateTime.Now;
                            customerWallet.CreatedBy = dto.CustomerId;
                            customerWallet.CustomerId = dto.CustomerId;
                            customerWallet.CreditLimit = dto.Amount;
                            _context.CustomerWallet.Add(customerWallet);
                            CustomerWalletTransaction CWalletTransaction = new CustomerWalletTransaction();
                            CWalletTransaction.ModifiedDate = DateTime.Now;
                            CWalletTransaction.ModifiedBy = dto.CustomerId;
                            CWalletTransaction.TransactionAmount = dto.Amount;
                            CWalletTransaction.TransactionType = "Deposit";
                            CWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                            _context.CustomerWalletTransaction.Add(CWalletTransaction);
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync(); //Execute when both tables data will inserted.
                            responseobj.Data = dto;
                            responseobj.Status.Code = (int)HttpStatusCode.OK;
                            responseobj.Status.Message = "Customer Wallet Create Succefully.";
                            responseobj.Status.Response = "Success";
                        }
                    }
                    else {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                        responseobj.Status.Message = "Invalid request";
                        responseobj.Status.Response = "failed";
                    }
                }
                catch (Exception ex)
                {
                    
                    await transaction.RollbackAsync();
                }
                return responseobj;

            }
        }
        public async Task<Response<OrderPaymentRequest>> OrderWithPayment(OrderPaymentRequest dto)
        {
            Response<OrderPaymentRequest> responseobj = new Response<OrderPaymentRequest>();
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
                            if (dto.TransactionMode.ToLower() == "creditcard")
                            {
                                orderPayment.TransactionModeId = 0;
                                // orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                            }
                            else if (dto.TransactionMode.ToLower() == "debitcard")
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
                                //  transaction.Rollback();
                                responseobj.Data = null;
                                responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                                responseobj.Status.Message = "Customer is not select Transaction Mode.";
                                responseobj.Status.Response = "Failed";
                                //throw new Exception("Customer is not select Transaction Mode.");
                            }
                            // OrderpaymentDataInserted = true;
                            // orderPayment = new OrderPayment();
                            OrderPayment orderPaymentData = _mapper.Map<OrderPaymentRequest, OrderPayment>(dto);
                            orderPaymentData.CustomerId = dto.CustomerId;
                            orderPaymentData.CreatedBy = dto.CustomerId;
                            orderPaymentData.CreatedDate = DateTime.Now;
                            orderPaymentData.CustomerWalletId = customerWallet.CustomerWalletId;
                            orderPaymentData.Amount = dto.OrderAmount;
                            orderPaymentData.TransactionModeId = orderPayment.TransactionModeId;
                            _context.OrderPayment.Add(orderPaymentData);
                            // _context.Entry(orderPayment).CurrentValues.SetValues(dto);
                            int id = await _context.SaveChangesAsync();
                            if (id > 0 && dto.TransactionMode != null)
                            {
                               
                                CustomerWalletTransaction customerWalletTransaction = new CustomerWalletTransaction();
                                customerWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                                customerWalletTransaction.TransactionAmount = dto.OrderAmount;
                                customerWalletTransaction.CreatedDate = DateTime.Now;
                                customerWalletTransaction.CreatedBy = dto.CustomerId;
                                customerWalletTransaction.CardNumber = dto.transactionModeNumber;
                                customerWalletTransaction.TransactionModeId = orderPayment.TransactionModeId;
                                customerWalletTransaction.CustomerId = dto.CustomerId;
                                customerWalletTransaction.TransactionType = "OrderPayment";
                                _context.CustomerWalletTransaction.Add(customerWalletTransaction);
                                await _context.SaveChangesAsync();

                                CustomerWalletTransactionDetail CustTranDetails = new CustomerWalletTransactionDetail();
                                CustTranDetails.ReferenceId = dto.OrderId;
                                CustTranDetails.Amount = Convert.ToInt32("-" + dto.OrderAmount);
                                CustTranDetails.CreatedDate = DateTime.Now;
                                CustTranDetails.CustomerId = dto.CustomerId;
                                CustTranDetails.CreatedBy = dto.CustomerId;
                                CustTranDetails.CustomerWalletTransactionId = customerWalletTransaction.CustomerWalletTransactionId;
                                _context.CustomerWalletTransactionDetail.Add(CustTranDetails);
                            }

                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();//Execute when both tables data will inserted.
                            responseobj.Data = dto;
                            responseobj.Status.Code = (int)HttpStatusCode.OK;
                            responseobj.Status.Message = "Order Successfully Placed";
                            responseobj.Status.Response = "Success";
                        }
                        else
                        {
                            responseobj.Data = null;
                            responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                            responseobj.Status.Message = "Customer is not exists.";
                            responseobj.Status.Response = "Failed";
                        }
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                        responseobj.Status.Message = "Invalid request";
                        responseobj.Status.Response = "failed";
                    }

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return responseobj;
            }
        }
        public async Task<Response<OrderWithOutPaymentRequest>> OrderWithOutPayment(OrderWithOutPaymentRequest dto)
        {
            Response<OrderWithOutPaymentRequest> responseobj = new Response<OrderWithOutPaymentRequest>();
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
                            orderPaymentData.CustomerId = dto.CustomerId;
                            orderPaymentData.CreatedDate = DateTime.Now; 
                            orderPaymentData.CustomerWalletId = customerWallet.CustomerWalletId;
                            _context.OrderPayment.Add(orderPaymentData);
                            int id = await _context.SaveChangesAsync();
                            if (id > 0 && dto.CustomerId != null)
                            {
                                CustomerWalletTransactionDetail CustTranDetails = new CustomerWalletTransactionDetail();
                                CustomerWalletTransaction customerWalletTransaction = new CustomerWalletTransaction();
                                customerWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                                customerWalletTransaction.CustomerId = dto.CustomerId;
                                customerWalletTransaction.TransactionAmount = dto.OrderAmount;
                                customerWalletTransaction.CreatedDate = DateTime.Now;
                                customerWalletTransaction.CreatedBy = dto.CustomerId;
                                customerWalletTransaction.TransactionType = "Order";
                                _context.CustomerWalletTransaction.Add(customerWalletTransaction);
                                await _context.SaveChangesAsync();
                                CustTranDetails.ReferenceId = dto.OrderId;
                                CustTranDetails.Amount = dto.OrderAmount;
                                CustTranDetails.CreatedDate = DateTime.Now;
                                CustTranDetails.CreatedBy = dto.CustomerId;
                                CustTranDetails.CustomerWalletTransactionId = customerWalletTransaction.CustomerWalletTransactionId;
                                CustTranDetails.CustomerId = dto.CustomerId;
                                _context.CustomerWalletTransactionDetail.Add(CustTranDetails);
                            }
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();//Execute when both tables data will inserted.
                            responseobj.Data = dto;
                            responseobj.Status.Code = (int)HttpStatusCode.OK;
                            responseobj.Status.Message = "Order Successfully Placed";
                            responseobj.Status.Response = "Success";
                        }
                        else
                        {
                            responseobj.Data = null;
                            responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                            responseobj.Status.Message = "Customer is not exists.";
                            responseobj.Status.Response = "Failed";
                        }
                     
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                        responseobj.Status.Message = "Invalid request";
                        responseobj.Status.Response = "failed";
                    }
                }
                catch (Exception ex)
                {
                    responseobj.Data = null;
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = ex.ToString();
                    responseobj.Status.Response = "failed";
                    await transaction.RollbackAsync();
                }
                return responseobj;
            }
        }
    }
}
