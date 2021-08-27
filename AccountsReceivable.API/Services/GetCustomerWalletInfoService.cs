using AccountsReceivable.API.Data;
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
        public async Task<List<CustomerWalletInfo>> GetCustomerWalletInfo(int customerid)
        {
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerWalletInformations;
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        if (dto != null)
            //        {
            //            CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);
            //            // bool isNewCustomerWallet = false;
            //            CustomerWallet customerWalletData = _mapper.Map<UpdateTransaction, CustomerWallet>(dto);
            //            if (customerWallet != null)
            //            {
            //                customerWallet.ModifiedDate = DateTime.UtcNow;
            //                customerWallet.ModifiedBy = dto.CustomerId;
            //                customerWallet.CreditLimit = dto.Amount;
            //                _context.Entry(customerWallet).CurrentValues.SetValues(dto);
            //                CustomerWalletTransaction CWalletTransaction = new CustomerWalletTransaction();
            //                CWalletTransaction.ModifiedDate = DateTime.UtcNow;
            //                CWalletTransaction.ModifiedBy = customerWallet.CustomerId;
            //                CWalletTransaction.TransactionAmount = dto.Amount;
            //                CWalletTransaction.TransactionType = "Deposit";
            //                CWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
            //                _context.CustomerWalletTransaction.Add(CWalletTransaction);
            //                await _context.SaveChangesAsync();
            //                await transaction.CommitAsync(); //Execute when both tables data will inserted.
            //                return dto;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        await transaction.RollbackAsync();
            //    }
            //    return dto;

            //}
        }


    }
}
