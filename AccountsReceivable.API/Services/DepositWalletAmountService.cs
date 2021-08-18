
using AccountsReceivable.API.Data;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services
{
    public class DepositWalletAmountService : IDepositWalletAmountService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;

        public DepositWalletAmountService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DepositWalletAmountVM> AddUpdateDepositWalletAmount(DepositWalletAmountVM dto)
        {
            if (dto != null)
            {
              //  DepositWalletAmount depositWalletAmount = await _context.DepositWalletAmount.FirstOrDefaultAsync(x => x.CustomerWalletId == dto.CustomerWalletId);
                DepositWalletAmount depositWalletAmount = await _context.DepositWalletAmount.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);
                if (depositWalletAmount == null)
                {
                    depositWalletAmount = new DepositWalletAmount();
                    DepositWalletAmount depositWalletAmountData = _mapper.Map<DepositWalletAmountVM, DepositWalletAmount>(dto);
                    _context.DepositWalletAmount.Add(depositWalletAmountData);
                }
                else
                {
                    _context.Entry(depositWalletAmount).CurrentValues.SetValues(dto);
                }
                await _context.SaveChangesAsync();
                return dto;
            }
            return dto;
        }
    }
}
