using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                if (dto != null)
                {
                    CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);
                    string RequestStatus = string.Empty;
                    SqlConnection conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);

                    SqlDataAdapter adapter;
                    DataSet ds = new DataSet();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlCommand cmd = new SqlCommand("CustomerDepositAmount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", dto.CustomerId);
                    cmd.Parameters.AddWithValue("@Amount", dto.Amount);

                    await cmd.ExecuteNonQueryAsync();

                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(RequestStatus))
                    {
                        if (RequestStatus.ToLower().Contains("successfully"))
                        {
                            responseobj.Data = dto;
                            responseobj.Status.Code = (int)HttpStatusCode.OK;
                            responseobj.Status.Message = customerWallet != null ? "Customer Wallet updated succesfully." : "Customer Wallet created succesfully.";
                            responseobj.Status.Response = "Success";
                        }
                        else
                        {
                            responseobj.Data = null;
                            responseobj.Status.Code = (int)HttpStatusCode.BadRequest;
                            responseobj.Status.Message = RequestStatus;
                            responseobj.Status.Response = "failed";
                        }
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
            catch (Exception)
            {
                responseobj.Data = null;
                responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                responseobj.Status.Message = "Invalid request";
                responseobj.Status.Response = "failed";
            }
            return responseobj;

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
                        string RequestStatus = string.Empty;
                        string TransactionModeId = string.Empty;
                        SqlConnection conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);

                        SqlDataAdapter adapter;
                        DataSet ds = new DataSet();
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlCommand cmd = new SqlCommand("sp_OrderWithPayment", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", dto.OrderId);
                        cmd.Parameters.AddWithValue("@OrderAmount", dto.OrderAmount);
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
                        cmd.Parameters.AddWithValue("@TransactionMode", TransactionModeId);
                        cmd.Parameters.AddWithValue("@transactionModeNumber", dto.transactionModeNumber);
                        cmd.Parameters.AddWithValue("@CustomerId", dto.CustomerId);

                        await cmd.ExecuteNonQueryAsync();

                        adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);

                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            if (i == 0)
                            {
                                RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                                if (RequestStatus.ToLower().Contains("successfully"))
                                {
                                    RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                                    responseobj.Data = dto;
                                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                                    responseobj.Status.Message = RequestStatus;
                                    responseobj.Status.Response = "Success";
                                    break;
                                }
                                else {
                                    RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                                    responseobj.Data = null;
                                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                                    responseobj.Status.Message = RequestStatus;
                                    responseobj.Status.Response = "FAILED";
                                }
                            }
                        }

                        //if (!string.IsNullOrEmpty(RequestStatus))
                        //{
                        //    if (RequestStatus.ToLower().Contains("successfully"))
                        //    {
                        //        responseobj.Data = dto;
                        //        responseobj.Status.Code = (int)HttpStatusCode.OK;
                        //        responseobj.Status.Message = customerWallet != null ? "Customer Wallet updated succesfully." : "Customer Wallet created succesfully.";
                        //        responseobj.Status.Response = "Success";
                        //    }
                        //    else
                        //    {
                        //        responseobj.Data = null;
                        //        responseobj.Status.Code = (int)HttpStatusCode.BadRequest;
                        //        responseobj.Status.Message = RequestStatus;
                        //        responseobj.Status.Response = "failed";
                        //    }
                        //}
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
                        string RequestStatus = string.Empty;
                        string TransactionModeId = string.Empty;
                        SqlConnection conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);

                        SqlDataAdapter adapter;
                        DataSet ds = new DataSet();
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlCommand cmd = new SqlCommand("sp_OrderWithOutPayment", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", dto.OrderId);
                        cmd.Parameters.AddWithValue("@OrderAmount", dto.OrderAmount);
                        cmd.Parameters.AddWithValue("@CustomerId", dto.CustomerId);

                        await cmd.ExecuteNonQueryAsync();

                        adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);

                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            if (i == 0)
                            {
                                RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                                if (RequestStatus.ToLower().Contains("successfully"))
                                {
                                    RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                                    responseobj.Data = dto;
                                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                                    responseobj.Status.Message = RequestStatus;
                                    responseobj.Status.Response = "Success";
                                    break;
                                }
                                else
                                {
                                    RequestStatus = ds.Tables[0].Rows[i][0].ToString();
                                    responseobj.Data = null;
                                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                                    responseobj.Status.Message = RequestStatus;
                                    responseobj.Status.Response = "FAILED";
                                }
                            }
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
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = ex.ToString();
                    responseobj.Status.Response = "FAILED";
                    await transaction.RollbackAsync();
                }
                return responseobj;
            }
        }
    }
}
