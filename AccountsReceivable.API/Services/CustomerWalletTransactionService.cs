using AccountsReceivable.API.Data;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AccountsReceivable.API.Models;
using System.Collections.Generic;

namespace AccountsReceivable.API.Services
{
    public class CustomerWalletTransactionService : ICustomerWalletTransactionService
    {
        #region Declarations
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CustomerWalletTransactionService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 
        #endregion

        public async Task<CustomerWalletTransactionVM> AddUpdateCustomerWalletTransaction(CustomerWalletTransactionVM dto)
        {
            if (dto != null)
            {
                CustomerWalletTransaction CustomerWalletTransaction = await _context.CustomerWalletTransaction.FirstOrDefaultAsync(x => x.CustomerWalletTransactionId == dto.CustomerWalletTransactionId);
                if (CustomerWalletTransaction == null)
                {
                    CustomerWalletTransaction = new CustomerWalletTransaction();
                    CustomerWalletTransaction CustomerWalletTransactionData = _mapper.Map<CustomerWalletTransactionVM, CustomerWalletTransaction>(dto);
                    _context.CustomerWalletTransaction.Add(CustomerWalletTransactionData);
                }
                else
                {
                    _context.Entry(CustomerWalletTransaction).CurrentValues.SetValues(dto);
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
                CustomerWalletTransaction CustomerWalletTransaction = _context.CustomerWalletTransaction.Find(id);
                if (CustomerWalletTransaction != null)
                {
                    _context.CustomerWalletTransaction.Remove(CustomerWalletTransaction);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<CustomerWalletTransactionVM>> GetCustomerWalletTransactions()
        {
            List<CustomerWalletTransaction> CustomerWalletTransactions = await _context.CustomerWalletTransaction.ToListAsync();

            List<CustomerWalletTransactionVM> data = _mapper.Map<List<CustomerWalletTransaction>, List<CustomerWalletTransactionVM>>(CustomerWalletTransactions);

            return data;
        }
        public async Task<CustomerWalletTransactionVM> GetCustomerWalletTransactionById(int id)
        {
            CustomerWalletTransaction CustomerWalletTransaction = await _context
                .CustomerWalletTransaction
                .SingleOrDefaultAsync(x => x.CustomerWalletTransactionId == id);

            return _mapper.Map<CustomerWalletTransaction, CustomerWalletTransactionVM>(CustomerWalletTransaction);
        }
    }
}
