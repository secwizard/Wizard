using AccountsReceivable.API.Data;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AccountsReceivable.API.Models;

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
            if (dto != null)
            {
                CustomerWallet customerWallet = await _context.CustomerWallet.FirstOrDefaultAsync(x => x.CustomerWalletId == dto.CustomerWalletId);
                if (customerWallet == null)
                {
                    customerWallet = new CustomerWallet();
                    CustomerWallet customerWalletData = _mapper.Map<CustomerWalletVM, CustomerWallet>(dto);
                    _context.CustomerWallet.Add(customerWalletData);
                }
                else
                {
                    _context.Entry(customerWallet).CurrentValues.SetValues(dto);
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
