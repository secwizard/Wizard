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

namespace AccountsReceivable.API.Services
{
    public class GetCustomerWalletInfoService : IGetCustomerWalletInfoService
    {
        private readonly AccountReceivableDataContext _context;
        public GetCustomerWalletInfoService(AccountReceivableDataContext context)
        {
            _context = context;
        }
        public async Task<Response<ResponseGetCustomerWalletInfo>> GetCustomerWalletInfo(int customerid)
        {
            Response<ResponseGetCustomerWalletInfo> response = new Response<ResponseGetCustomerWalletInfo>();

            try
            {
                var parmsList = new SqlParameter[] { new SqlParameter("@customerId",customerid) };

                string sqlText = $"EXECUTE dbo.GetCustomerWalletInfo @customerId";
                var result = await _context.CustomerWalletInfo.FromSqlRaw(sqlText, parmsList).ToListAsync();

                string sqlText2 = $"EXECUTE dbo.GetCustomerWalletTransactionInfo @customerId";
                var resultList = await _context.CustomerWalletTransactionList.FromSqlRaw(sqlText2, parmsList).ToListAsync();

                if (result != null && resultList != null)
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
    }
}
