using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
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
                        new SqlParameter("@TransactionMode", dto.TransactionMode),
                        new SqlParameter("@CardNumber", dto.CardNumber??"")
                    };

                    string sqlText = $"EXECUTE dbo.CustomerDepositAmount @customerId, @Amount, @TransactionMode, @CardNumber";
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
                        responseobj.Status.Message = result.FirstOrDefault().Msg;
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
                    string RequestStatus = string.Empty;
                    string TransactionModeId = string.Empty;
                    if (dto.TransactionMode.ToLower() == "creditcard")
                    {
                        TransactionModeId = 0.ToString();
                        // orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                    }
                    else if (dto.TransactionMode.ToLower() == "debitcard")
                    {
                        TransactionModeId = 1.ToString();
                        //    orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                    }
                    else if (dto.TransactionMode.ToLower() == "cheque")
                    {
                        TransactionModeId = 2.ToString();
                        //  orderPayment.TransactionModeNumber = dto.transactionModeNumber;
                    }
                    else if (dto.TransactionMode.ToLower() == "cash")
                    {
                        TransactionModeId = 3.ToString();
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

                    string OrderWithPaymentResult = string.Empty;
                    var CustomerIdSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@CustomerId", dto.CustomerId);
                    var OrderAmountSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@Amount", dto.OrderAmount);
                    var TransactionModeSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@TransactionMode", TransactionModeId);
                    var transactionModeNumberSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@transactionModeNumber", dto.transactionModeNumber);
                    var OrderIdSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@OrderId", dto.OrderId);

                    var OrderWithPaymentResultSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@OrderWithPaymentResult", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.sp_OrderWithPayment @OrderId={0},@OrderAmount={1},@TransactionMode={2},@transactionModeNumber={3},@CustomerId={4},@OrderWithPaymentResult={5} out", OrderIdSQLParam, OrderAmountSQLParam, TransactionModeSQLParam, transactionModeNumberSQLParam, CustomerIdSQLParam, OrderWithPaymentResultSQLParam);

                    if (OrderWithPaymentResultSQLParam.Value != DBNull.Value)
                    {
                        OrderWithPaymentResult = (string)OrderWithPaymentResultSQLParam.Value;
                    }

                    if (OrderWithPaymentResult.ToLower().Contains("placed"))
                    {
                        RequestStatus = OrderWithPaymentResult;
                        responseobj.Data = dto;
                        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        responseobj.Status.Message = RequestStatus;
                        responseobj.Status.Response = "Success";
                    }
                    else
                    {
                        RequestStatus = OrderWithPaymentResult;
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                        responseobj.Status.Message = RequestStatus;
                        responseobj.Status.Response = "FAILED";
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

                    string OrderWithoutpaymentResult = string.Empty;
                    var OrderIdSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@OrderId", dto.OrderId);
                    var OrderAmountSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@OrderAmount", dto.OrderAmount);
                    var CustomerIdSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@CustomerId", dto.CustomerId);

                    var OrderWithPaymentResultSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@OrderWithoutPaymentResult", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.sp_OrderWithOutPayment @OrderId={0},@OrderAmount={1},@CustomerId={2},@OrderWithoutPaymentResult={3} out", OrderIdSQLParam, OrderAmountSQLParam, CustomerIdSQLParam, OrderWithPaymentResultSQLParam);

                    if (OrderWithPaymentResultSQLParam.Value != DBNull.Value)
                    {
                        OrderWithoutpaymentResult = (string)OrderWithPaymentResultSQLParam.Value;
                    }


                    if (OrderWithoutpaymentResult.ToLower().Contains("placed"))
                    {
                        responseobj.Data = dto;
                        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        responseobj.Status.Message = OrderWithoutpaymentResult;
                        responseobj.Status.Response = "Success";
                    }
                    else
                    {
                        responseobj.Data = null;
                        responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                        responseobj.Status.Message = OrderWithoutpaymentResult;
                        responseobj.Status.Response = "FAILED";
                    }

                    // var OrderpaymentDataInserted = false;


                }
                else
                {
                    responseobj.Data = null;
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = "Invalid request";
                    responseobj.Status.Response = "FAILED";
                }
            }
            catch (Exception ex)
            {
                responseobj.Data = null;
                responseobj.Status.Code = (int)HttpStatusCode.InternalServerError;
                responseobj.Status.Message = ex.Message.ToString();
                responseobj.Status.Response = "FAILED";
            }
            return responseobj;
        }
    }
}
