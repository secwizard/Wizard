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
    public class CashBackMasterService : ICashBackMasterService
    {
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        public CashBackMasterService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CashBackMasterVM> AddOrUpdateCashBackMaster(CashBackMasterVM dto)
        {
            if (dto != null)
            {
                CashBackMasters CashBackMaster = await _context.CashBackMaster.FirstOrDefaultAsync(x => x.CashBackMasterId == dto.CashBackMasterId);
                if (CashBackMaster == null)
                {
                    CashBackMaster = new CashBackMasters();
                    CashBackMasters CashBackMasterData = _mapper.Map<CashBackMasterVM, CashBackMasters>(dto);
                    _context.CashBackMaster.Add(CashBackMasterData);
                }
                else
                {
                    _context.Entry(CashBackMaster).CurrentValues.SetValues(dto);
                }
                await _context.SaveChangesAsync();
                return dto;
            }
            return dto;
        }
        public async Task<List<CashBackMasterVM>> GetCashBackMaster()
        {
            List<CashBackMasters> cashBackMaster = await _context.CashBackMaster.ToListAsync();

            List<CashBackMasterVM> data = _mapper.Map<List<CashBackMasters>, List<CashBackMasterVM>>(cashBackMaster);

            return data;
        }
        public async Task<CashBackMasterVM> GetCashBackMasterById(int id)
        {
            CashBackMasters cashBackMaster = await _context
               .CashBackMaster
               .SingleOrDefaultAsync(x => x.CashBackMasterId == id);
            return _mapper.Map<CashBackMasters, CashBackMasterVM>(cashBackMaster);
        }
        public async Task Delete(int id)
        {
            if (id > 0)
            {
                CashBackMasters cashbackMaster = _context.CashBackMaster.Find(id);
                if (cashbackMaster != null)
                {
                    _context.CashBackMaster.Remove(cashbackMaster);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
