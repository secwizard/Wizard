using AccountsReceivable.API.Data;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.Services.Interface;
using AccountsReceivable.API.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services
{
    public class CashBackTransactionService : ICashBackTransactionService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        public CashBackTransactionService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CashBackTransactionVM> AddOrUpdateCashBackTransaction(CashBackTransactionVM dto)
        {
            if (dto != null)
            {
                CashBackTransaction CashBackTransaction = await _context.CashBackTransaction.FirstOrDefaultAsync(x => x.CashBackTransactionId == dto.CashBackTransactionId);
                if (CashBackTransaction == null)
                {
                    CashBackTransaction = new CashBackTransaction();
                    CashBackTransaction CashbackTransactiondata = _mapper.Map<CashBackTransactionVM, CashBackTransaction>(dto);
                    _context.CashBackTransaction.Add(CashbackTransactiondata);
                }
                else
                {
                    _context.Entry(CashBackTransaction).CurrentValues.SetValues(dto);
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
                CashBackTransaction cashBackTransaction = _context.CashBackTransaction.Find(id);
                if (cashBackTransaction != null)
                {
                    _context.CashBackTransaction.Remove(cashBackTransaction);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<CashBackTransactionVM>> GetCashBackTransaction()
        {
            List<CashBackTransaction> cashBackTransaction = await _context.CashBackTransaction.ToListAsync();

            List<CashBackTransactionVM> data = _mapper.Map<List<CashBackTransaction>, List<CashBackTransactionVM>>(cashBackTransaction);

            return data;
        }
        public async Task<CashBackTransactionVM> GetCashBackTransactionById(int id)
        {
            CashBackTransaction cashbackTransaction = await _context
               .CashBackTransaction
               .SingleOrDefaultAsync(x => x.CashBackTransactionId == id);
            return _mapper.Map<CashBackTransaction, CashBackTransactionVM>(cashbackTransaction);
        }
    }
}
