using AccountsReceivable.API.Data;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AccountsReceivable.API.Models;
using System;

namespace AccountsReceivable.API.Services
{
    public class CustomerWalletService : ICustomerWalletService
    {
        #region Declarations
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public CustomerWalletService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public async Task<CustomerWalletVM> AddUpdateCustomerWallet(CustomerWalletVM dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (dto != null)
                    {
                        CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerWalletId == dto.CustomerWalletId);
                        bool isNewCustomerWallet = false;
                        CustomerWallet customerWalletData = _mapper.Map<CustomerWalletVM, CustomerWallet>(dto);
                        if (customerWallet == null)
                        {
                            customerWallet = new CustomerWallet();
                            isNewCustomerWallet = true;
                            customerWallet.CreatedDate = DateTime.UtcNow;
                            _context.CustomerWallet.Add(customerWalletData);
                        }
                        else
                        {
                            isNewCustomerWallet = false;
                            customerWallet.ModifiedDate = DateTime.UtcNow;
                            _context.Entry(customerWallet).CurrentValues.SetValues(dto);
                        }
                        int id = await _context.SaveChangesAsync();
                        if (isNewCustomerWallet && id > 0 && customerWallet.CreditLimit != null)
                        {
                            CustomerWalletTransaction CWalletTransaction = new CustomerWalletTransaction();
                            CWalletTransaction.CreatedDate = DateTime.UtcNow;
                            CWalletTransaction.TransactionAmount = dto.CreditLimit;
                            CWalletTransaction.TransactionType = "Deposit";
                            CWalletTransaction.CustomerWalletId = customerWallet.CustomerWalletId;
                            _context.CustomerWalletTransaction.Add(CWalletTransaction);
                            await _context.SaveChangesAsync();
                        }
                        await transaction.CommitAsync(); //Execute when both tables data will inserted.
                        return dto;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return dto;
            }

        }
        public async Task Delete(int id)
        {
            if (id > 0)
            {
                CustomerWallet customerWallet = _context.CustomerWallet.Find(id);
                if (customerWallet != null)
                {
                    _context.CustomerWallet.Remove(customerWallet);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<CustomerWalletVM>> GetCustomerWallet()
        {
            List<CustomerWallet> customerWallet = await _context.CustomerWallet.ToListAsync();

            List<CustomerWalletVM> data = _mapper.Map<List<CustomerWallet>, List<CustomerWalletVM>>(customerWallet);

            return data;
        }
        public async Task<CustomerWalletVM> GetCustomerWalletById(int id)
        {
            CustomerWallet customerWallet = await _context
               .CustomerWallet
               .SingleOrDefaultAsync(x => x.CustomerWalletId == id);
            return _mapper.Map<CustomerWallet, CustomerWalletVM>(customerWallet);
        }
    }
}
