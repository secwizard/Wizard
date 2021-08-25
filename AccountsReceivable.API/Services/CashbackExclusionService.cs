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
    public class CashbackExclusionService : ICashbackExclusionService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        public CashbackExclusionService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CashbackExclusionVM> AddOrUpdateCashbackExclusion(CashbackExclusionVM dto)
        {
            if (dto != null)
            {
                CashbackExclusion CashbackExclusions = await _context.CashbackExclusion.FirstOrDefaultAsync(x => x.CashbackExclusionId == dto.CashbackExclusionId);
                if (CashbackExclusions == null)
                {
                    CashbackExclusions = new CashbackExclusion();
                    CashbackExclusion CashbackTransactiondata = _mapper.Map<CashbackExclusionVM, CashbackExclusion>(dto);
                    _context.CashbackExclusion.Add(CashbackTransactiondata);
                }
                else
                {
                    _context.Entry(CashbackExclusions).CurrentValues.SetValues(dto);
                }
                await _context.SaveChangesAsync();
                return dto;
            }
            return dto;
        }
        public async Task<List<CashbackExclusionVM>> GetCashbackExclusion()
        {
            List<CashbackExclusion> cashbackExclusions = await _context.CashbackExclusion.ToListAsync();

            List<CashbackExclusionVM> data = _mapper.Map<List<CashbackExclusion>, List<CashbackExclusionVM>>(cashbackExclusions);

            return data;
        }
        public async Task<CashbackExclusionVM> GetCashbackExclusionById(int id)
        {
            CashbackExclusion cashbackExclusion = await _context
               .CashbackExclusion
               .SingleOrDefaultAsync(x => x.CashbackExclusionId == id);
            return _mapper.Map<CashbackExclusion, CashbackExclusionVM>(cashbackExclusion);
        }
        public async Task Delete(int id)
        {
            if (id > 0)
            {
                CashbackExclusion cashbackExclusion = _context.CashbackExclusion.Find(id);
                if (cashbackExclusion != null)
                {
                    _context.CashbackExclusion.Remove(cashbackExclusion);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
