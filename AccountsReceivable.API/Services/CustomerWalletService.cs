using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using AccountsReceivable.API.Models.ResponseModel;
using System.Linq;
using AccountsReceivable.API.Models.RequestModel;

namespace AccountsReceivable.API.Services
{
    public class CustomerWalletService : ICustomerWalletService
    {
        private readonly AccountReceivableDataContext _context;
        public CustomerWalletService(AccountReceivableDataContext context)
        {
            _context = context;
        }

        public async Task<Response<UpdateTransaction>> CreateNewCustomerWallet(UpdateTransaction dto)
        {
            Response<UpdateTransaction> responseobj = new Response<UpdateTransaction>();
            try
            {
                if (dto != null)
                {

                    var parmsList = new SqlParameter[] {
                        new SqlParameter("@customerId", dto.CustomerId),
                        new SqlParameter("@Amount", dto.Amount),
                        new SqlParameter("@UserId", dto.UserId)
                    };

                    string sqlText = $"EXECUTE dbo.CreateNewCustomerWallet @customerId, @Amount, @UserId";
                    var result = await _context.CustomerDepositAmount.FromSqlRaw(sqlText, parmsList).ToListAsync();


                    if (result != null && result.Count > 0 && (result?.FirstOrDefault()?.Id ?? 0) == 1)
                    {
                        responseobj.Data = dto;
                        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        responseobj.Status.Message = result.FirstOrDefault().Msg;
                        responseobj.Status.Response = "success";
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.BadRequest;
                        responseobj.Status.Message = result?.FirstOrDefault()?.Msg ?? "failed";
                        responseobj.Status.Response = "failed";
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
                responseobj.Status.Code = (int)HttpStatusCode.InternalServerError;
                responseobj.Status.Message = ex.Message.ToString();
                responseobj.Status.Response = "failed";
            }
            return responseobj;

        }
        public async Task<Response<ResponseGetCustomerWalletInfo>> GetCustomerWalletInfo(int customerid)
        {
            Response<ResponseGetCustomerWalletInfo> response = new Response<ResponseGetCustomerWalletInfo>();

            try
            {
                var parmsList = new SqlParameter[] { new SqlParameter("@customerId", customerid) };

                string sqlText = $"EXECUTE dbo.GetCustomerWalletInfo @customerId";
                var result = await _context.CustomerWalletInfo.FromSqlRaw(sqlText, parmsList).ToListAsync();

                string sqlText2 = $"EXECUTE dbo.GetCustomerWalletTransactionInfo @customerId";
                var resultList = await _context.CustomerWalletTransactionList.FromSqlRaw(sqlText2, parmsList).ToListAsync();

                if (result != null)
                {
                    ResponseGetCustomerWalletInfo responseobj = new ResponseGetCustomerWalletInfo
                    {
                        CustomerWalletInfo = result.FirstOrDefault(),
                        CustomerWalletTransactionList = resultList
                    };

                    response.Data = responseobj;
                    response.Status.Code = (int)HttpStatusCode.OK;
                    response.Status.Message = "Get successfully Information of customer wallet";
                    response.Status.Response = "success";
                }
                else
                {
                    response.Data = null;
                    response.Status.Code = (int)HttpStatusCode.NotFound;
                    response.Status.Message = "Customer is not exists.";
                    response.Status.Response = "failed";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status.Code = (int)HttpStatusCode.InternalServerError;
                response.Status.Message = ex.Message.ToString();
                response.Status.Response = "failed";
            }
            return response;
        }

        public async Task<Response<ResponseCheckCustomerWalletDetailForPlaceOrder>> CheckCustomerWalletDetailForPlaceOrder(CheckCustomerWalletDetailForPlaceOrder request)
        {
            Response<ResponseCheckCustomerWalletDetailForPlaceOrder> response = new Response<ResponseCheckCustomerWalletDetailForPlaceOrder>();

            try
            {
                var parmsList = new SqlParameter[] {
                    new SqlParameter("@customerId", request.CustomerId),
                    new SqlParameter("@OrderAmount", request.OrderAmount)
                };

                string sqlText = $"EXECUTE dbo.CheckCustomerWalletDetailForPlaceOrder @customerId, @OrderAmount";
                var result = await _context.CheckCustomerWalletDetailForPlaceOrder.FromSqlRaw(sqlText, parmsList).ToListAsync();

                if (result != null && result.Count > 0 && (result?.FirstOrDefault()?.Id ?? 0) == 1)
                {
                    response.Data = result.FirstOrDefault();
                    response.Status.Code = (int)HttpStatusCode.OK;
                    response.Status.Message = "Order Place";
                    response.Status.Response = "success";
                }
                else
                {
                    response.Data = null;
                    response.Status.Code = (int)HttpStatusCode.NotFound;
                    response.Status.Message = result?.FirstOrDefault()?.Msg ?? "Customer is not exists.";
                    response.Status.Response = "failed";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status.Code = (int)HttpStatusCode.InternalServerError;
                response.Status.Message = ex.Message.ToString();
                response.Status.Response = "failed";
            }
            return response;
        }

        public async Task<Response<ResponseSaveCustomerPayment>> SaveCustomerPayment(SaveCustomerPaymentRequest customerPaymentRequest)
        {
            Response<ResponseSaveCustomerPayment> responseobj = new Response<ResponseSaveCustomerPayment>();
            try
            {
                if (customerPaymentRequest != null)
                {
                    var parmsList = new SqlParameter[] {
                        new SqlParameter("@CompanyId", customerPaymentRequest.CompanyId),
                        new SqlParameter("@CustomerId", customerPaymentRequest.CustomerId),
                        new SqlParameter("@TransactionAmount", customerPaymentRequest.TransactionAmount),
                        new SqlParameter("@TransactionModeId", customerPaymentRequest.TransactionModeId),
                        new SqlParameter("@TransactionType", customerPaymentRequest.TransactionType),
                        new SqlParameter("@TransactionDate", customerPaymentRequest.TransactionDate),
                        new SqlParameter("@CardNumber", customerPaymentRequest.CardNumber??""),
                        new SqlParameter("@CardHolderName", customerPaymentRequest.CardHolderName??""),
                        new SqlParameter("@ChequeNo", customerPaymentRequest.ChequeNo??""),
                        new SqlParameter("@ChequeHolderName", customerPaymentRequest.ChequeHolderName??""),
                        new SqlParameter("@Note", customerPaymentRequest.Note??""),
                        new SqlParameter("@CreatedFrom", customerPaymentRequest.CreatedFrom??""),
                        new SqlParameter("@OrderDetails", customerPaymentRequest.OrderDetails??""),
                        new SqlParameter("@UserId", customerPaymentRequest.UserId),
                        new SqlParameter("@CreatedAt", customerPaymentRequest.CreatedAt)
                    };

                    string sqlText = $"EXECUTE dbo.SaveCustomerPayment @CompanyId, @CustomerId, @TransactionAmount, @TransactionModeId, @TransactionType, @TransactionDate, @CardNumber, @CardHolderName, @ChequeNo, @ChequeHolderName, @Note, @CreatedFrom, @OrderDetails, @UserId, @CreatedAt";
                    var result = await _context.SaveCustomerPayment.FromSqlRaw(sqlText, parmsList).ToListAsync();


                    if (result != null && result.Count > 0)
                    {
                        responseobj.Data = result[0];
                        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        responseobj.Status.Message = result.FirstOrDefault().Message;
                        responseobj.Status.Response = "success";
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.BadRequest;
                        responseobj.Status.Message = result?.FirstOrDefault()?.Message ?? "failed";
                        responseobj.Status.Response = "failed";
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
                responseobj.Status.Code = (int)HttpStatusCode.InternalServerError;
                responseobj.Status.Message = ex.Message.ToString();
                responseobj.Status.Response = "failed";
            }
            return responseobj;

        }

    }
}
