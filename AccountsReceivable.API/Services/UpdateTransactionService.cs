using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Models.ResponseModel;
using AccountsReceivable.API.Services.Interface;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services
{
    public class UpdateTransactionService : IUpdateTransactionService
    {
        private readonly AccountReceivableDataContext _context;
        public UpdateTransactionService(AccountReceivableDataContext context)
        {
            _context = context;
        }
        public async Task<Response<UpdateTransaction>> CustomerDepositAmount(UpdateTransaction dto)
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

                    string sqlText = $"EXECUTE dbo.CustomerDepositAmount @customerId, @Amount, @UserId";
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
        public async Task<Response<OrderPaymentRequest>> OrderWithPayment(OrderPaymentRequest dto)
        {
            Response<OrderPaymentRequest> responseobj = new Response<OrderPaymentRequest>();
            try
            {
                if (dto != null)
                {
                    var parmsList = new SqlParameter[] {
                        new SqlParameter("@OrderId", dto.OrderId),
                        new SqlParameter("@OrderAmount", dto.OrderAmount),
                        new SqlParameter("@TransactionMode", dto.TransactionMode??""),
                        new SqlParameter("@transactionModeNumber", dto.transactionModeNumber??""),
                        new SqlParameter("@customerId", dto.CustomerId),
                        new SqlParameter("@UserId", dto.UserId)
                    };

                    string sqlText = $"EXECUTE dbo.sp_OrderWithPayment @OrderId, @OrderAmount, @TransactionMode, @transactionModeNumber, @CustomerId, @UserId";
                    var result = await _context.OrderWithPayment.FromSqlRaw(sqlText, parmsList).ToListAsync();

                    if (result != null && result.Count > 0 && (result?.FirstOrDefault()?.Id ?? 0) == 1)
                    {

                        responseobj.Data = dto;
                        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        responseobj.Status.Message = result.FirstOrDefault().Msg;
                        responseobj.Status.Response = "Success";
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                        responseobj.Status.Message = result?.FirstOrDefault()?.Msg ?? "Data Not Found";
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
        public async Task<Response<OrderWithOutPaymentRequest>> OrderWithOutPayment(OrderWithOutPaymentRequest dto)
        {
            Response<OrderWithOutPaymentRequest> responseobj = new Response<OrderWithOutPaymentRequest>();

            try
            {
                if (dto != null)
                {
                    string TransactionModeId = string.Empty;

                    var parmsList = new SqlParameter[] {
                        new SqlParameter("@OrderId", dto.OrderId),
                        new SqlParameter("@OrderAmount", dto.OrderAmount),
                        new SqlParameter("@customerId", dto.CustomerId),
                        new SqlParameter("@UserId", dto.UserId)
                    };

                    string sqlText = $"EXECUTE dbo.sp_OrderWithOutPayment @OrderId, @OrderAmount, @CustomerId, @UserId";
                    var result = await _context.OrderWithPayment.FromSqlRaw(sqlText, parmsList).ToListAsync();

                    if (result != null && result.Count > 0 && (result?.FirstOrDefault()?.Id ?? 0) == 1)
                    {
                        responseobj.Data = dto;
                        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        responseobj.Status.Message = result?.FirstOrDefault()?.Msg ?? "";
                        responseobj.Status.Response = "success";
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
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

        public async Task<ResponseList<ResponseCustomerOrderPaymentList>> CustomerOrderPaymentList(CustomerOrderPaymentList request)
        {
            ResponseList<ResponseCustomerOrderPaymentList> responseobj = new ResponseList<ResponseCustomerOrderPaymentList>();
            try
            {
                var parmsList = new SqlParameter[] { new SqlParameter("@CustomerId", request.CustomerId) };
                string sqlText = $"EXECUTE dbo.CustomerOrderPaymentList @CustomerId";
                var result = await _context.CustomerOrderPaymentList.FromSqlRaw(sqlText, parmsList).ToListAsync();

                if (result != null && result.Count > 0)
                {
                    responseobj.Data = result;
                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                    responseobj.Status.Message = "";
                    responseobj.Status.Response = "Success";
                }
                else
                {
                    responseobj.Data = null;
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = "Data Not Found";
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
        public async Task<ResponseList<ResponseGetTransactionMode>> GetTransactionMode()
        {
            ResponseList<ResponseGetTransactionMode> responseobj = new ResponseList<ResponseGetTransactionMode>();
            try
            {
                string sqlText = $"EXECUTE dbo.GetTransactionMode";
                var result = await _context.GetTransactionMode.FromSqlRaw(sqlText,"").ToListAsync();

                if (result != null && result.Count > 0)
                {
                    responseobj.Data = result;
                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                    responseobj.Status.Message = "";
                    responseobj.Status.Response = "Success";
                }
                else
                {
                    responseobj.Data = null;
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = "Data Not Found";
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
