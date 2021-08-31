using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

            List<CustomerWalletInfo> customerWalletInformations = new List<CustomerWalletInfo>();

            try
            {
                SqlConnection conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
                SqlDataReader rdr = null;
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand("GetCustomerWalletInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customerId", customerid));

                rdr = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(rdr);
                if (dataTable.Rows.Count > 0)
                {
                    customerWalletInformations = (List<CustomerWalletInfo>)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dataTable), typeof(List<CustomerWalletInfo>));
                    responseobj.Data = customerWalletInformations;
                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                    responseobj.Status.Message = "Get successfully Information of customer wallet";
                    responseobj.Status.Response = "Success";
                }
                else {
                    responseobj.Data = null;
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = "Customer is not exists.";
                    responseobj.Status.Response = "Failed";
                }
            }
            catch (Exception ex)
            {
                responseobj.Data = null;
                responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                responseobj.Status.Message = ex.ToString();
                responseobj.Status.Response = "Failed";
            }
            return responseobj;
        }
    }
}
