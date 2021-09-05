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
namespace AccountsReceivable.API.Services
{
    public class GetCustomerWalletInfoService : IGetCustomerWalletInfoService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        public GetCustomerWalletInfoService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<CustomerWalletInfo>>> GetCustomerWalletInfo(int customerid)
        {
            Response<List<CustomerWalletInfo>> responseobj = new Response<List<CustomerWalletInfo>>();

            try
            {
                var parmsList = new SqlParameter[] {
                    new SqlParameter("@customerId",customerid),
                };
                string sqlTextList = $"EXECUTE dbo.GetCustomerWalletInfo @customerId";
                List<CustomerWalletInfo> response = await _context.CustomerWalletInfo.FromSqlRaw(sqlTextList, parmsList).ToListAsync();

                if (response != null && response.Count > 0)
                {
                    responseobj.Data = response;
                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                    responseobj.Status.Message = "Get successfully Information of customer wallet";
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
            catch (Exception ex)
            {
                responseobj.Data = null;
                responseobj.Status.Code = (int)HttpStatusCode.InternalServerError;
                responseobj.Status.Message = ex.Message.ToString();
                responseobj.Status.Response = "Failed";
            }
            return responseobj;
        }
    }
}
