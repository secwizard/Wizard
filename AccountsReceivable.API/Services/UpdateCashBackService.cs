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
                        if (dto.CompanyId != null)
                        {
                            string CashBackForCustomerResult = string.Empty;
                            var CompanyId = new Microsoft.Data.SqlClient.SqlParameter("@CompanyId", dto.CompanyId);
                            var MinimumBusinessAmount = new Microsoft.Data.SqlClient.SqlParameter("@MinimumBusinessAmount", dto.MinimumBusinessAmount);
                            var MaximumCashbackAmount = new Microsoft.Data.SqlClient.SqlParameter("@MaximumCashbackAmount", dto.MaximumCashbackAmount);
                            var StartDate = new Microsoft.Data.SqlClient.SqlParameter("@StartDate", dto.StartDate.ToString());
                            var EndDate = new Microsoft.Data.SqlClient.SqlParameter("@EndDate", dto.EndDate.ToString());
                            var CashbackValue = new Microsoft.Data.SqlClient.SqlParameter("@CashbackValue", dto.CashbackValue);
                            var IsPercentage = new Microsoft.Data.SqlClient.SqlParameter("@IsPercentage", dto.IsPercentage);
                            var IsActive = new Microsoft.Data.SqlClient.SqlParameter("@IsActive", dto.IsActive);

                            var CashBackForCustomerResultSQLParam = new Microsoft.Data.SqlClient.SqlParameter("@CashBackForCustomerResult", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                            await _context.Database.ExecuteSqlRawAsync("exec dbo.AddCashBack @CompanyId={0},@MinimumBusinessAmount={1},@MaximumCashbackAmount={2},@StartDate={3},@EndDate={4},@CashbackValue={5},@IsPercentage={6},@IsActive={7},@CashBackForCustomerResult={8} out", CompanyId, MinimumBusinessAmount, MaximumCashbackAmount, StartDate, EndDate, CashbackValue, IsPercentage, IsActive, CashBackForCustomerResultSQLParam);

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
        //public async Task<Response<CashbackDetail>> GetCashbackDetails(CashbackDetail dto)
        //{
        //    Response<CashbackMasterRequest> responseobj = new Response<CashbackMasterRequest>();

        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {

        //        }
        //    }
        //}
    }

}
