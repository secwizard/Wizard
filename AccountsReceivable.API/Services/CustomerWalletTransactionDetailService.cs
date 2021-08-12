using AccountsReceivable.API.Data;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services
{
    public class CustomerWalletTransactionDetailService: ICustomerWalletTransactionDetailService
    {
        #region Declarations
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CustomerWalletTransactionDetailService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public async Task<CustomerWalletTransactionDetailVM> AddUpdateCustomerWalletTransactionDetail(CustomerWalletTransactionDetailVM dto)
        {
            if (dto != null)
            {
                CustomerWalletTransactionDetail customerWalletTransactionDetail = await _context.CustomerWalletTransactionDetail.FirstOrDefaultAsync(x => x.CustomerWalletTransactionDetailId == dto.CustomerWalletTransactionDetailId);
                if (customerWalletTransactionDetail == null)
                {
                    customerWalletTransactionDetail = new CustomerWalletTransactionDetail();
                    CustomerWalletTransactionDetail CustomerWalletTransactionDetailData = _mapper.Map<CustomerWalletTransactionDetailVM, CustomerWalletTransactionDetail>(dto);
                    _context.CustomerWalletTransactionDetail.Add(CustomerWalletTransactionDetailData);
                }
                else
                {
                    _context.Entry(customerWalletTransactionDetail).CurrentValues.SetValues(dto);
                }
                await _context.SaveChangesAsync();
                return dto;
            }
            return dto;
        }
        public async Task Delete(int id)
        {
            if (id > 0)
            {
                CustomerWalletTransactionDetail customerWalletTransactionDetail = _context.CustomerWalletTransactionDetail.Find(id);
                if (customerWalletTransactionDetail != null)
                {
                    _context.CustomerWalletTransactionDetail.Remove(customerWalletTransactionDetail);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<CustomerWalletTransactionDetailVM>> GetCustomerWalletTransactionDetails()
        {
            var customerWalletTransaction = await _context.CustomerWalletTransactionDetail.ToListAsync();

            var data = _mapper.Map<List<CustomerWalletTransactionDetail>, List<CustomerWalletTransactionDetailVM>>(customerWalletTransaction);

            return data;
        }
        public async Task<CustomerWalletTransactionDetailVM> GetCustomerWalletTransactionDetailById(int id)
        {
            var customerWalletTransaction = await _context
               .CustomerWalletTransactionDetail
               .SingleOrDefaultAsync(x => x.CustomerWalletTransactionDetailId == id);
            return _mapper.Map<CustomerWalletTransactionDetail, CustomerWalletTransactionDetailVM>(customerWalletTransaction);
        }
    }
}
