using AccountsReceivable.API.Data;
using AccountsReceivable.API.Helpers;
using AccountsReceivable.API.Models.RequestModel;
using AccountsReceivable.API.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services
{
    public class UpdateCashBackService : IUpdateCashBackService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        public UpdateCashBackService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<CashbackMasterRequest>> AddCashBackForCustomer(CashbackMasterRequest dto)
        {
            Response<CashbackMasterRequest> responseobj = new Response<CashbackMasterRequest>();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (dto != null)
                    {
                        if (dto.CustomerId != null)
                        {
                            string CashBackForCustomerResult = string.Empty;
                            var CustomerIdSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@CustomerId", dto.CustomerId);
                            var CashBackPercentageSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@CashBackPercentage", dto.CashBackPercentage);

                            var CashBackForCustomerResultSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@CashBackForCustomerResult", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                            await _context.Database.ExecuteSqlRawAsync("exec dbo.AddCashBackForCustomer @CustomerId={0},@CashBackPercentage={1},@CashBackForCustomerResult={2} out", CustomerIdSQLParam, CashBackForCustomerResult, CashBackForCustomerResultSQLParam);

                            if (CashBackForCustomerResultSQLParam.Value != DBNull.Value)
                            {
                                CashBackForCustomerResult = (string)CashBackForCustomerResultSQLParam.Value;
                            }

                            if (!string.IsNullOrEmpty(CashBackForCustomerResult))
                            {
                                if (CashBackForCustomerResult.ToLower().Contains("successfully"))
                                {
                                    responseobj.Data = dto;
                                    responseobj.Status.Code = (int)HttpStatusCode.OK;
                                    responseobj.Status.Message = CashBackForCustomerResult;
                                    responseobj.Status.Response = "Success";
                                }
                                else
                                {
                                    responseobj.Data = null;
                                    responseobj.Status.Code = (int)HttpStatusCode.BadRequest;
                                    responseobj.Status.Message = CashBackForCustomerResult;
                                    responseobj.Status.Response = "failed";
                                }
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
                catch (Exception ex)
                {
                    responseobj.Data = null;
                    responseobj.Status.Code = (int)HttpStatusCode.NotFound;
                    responseobj.Status.Message = ex.ToString();
                    responseobj.Status.Response = "failed";
                    await transaction.RollbackAsync();
                   // throw new Exception(ex.Message.ToString());
                }
                return responseobj;
            }
        }
    }
}
