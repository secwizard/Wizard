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
    public class TransactionModeService: ITransactionModeService
    {
        #region Declarations
        private readonly AccountReceivableDataContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TransactionModeService(AccountReceivableDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        //public async Task<TransactionModeVM> AddUpdateTransactionMode(TransactionModeVM dto)
        //{
        //    if (dto != null)
        //    {
        //        TransactionMode transactionMode = await _context.TransactionMode.FirstOrDefaultAsync(x => x.TransactionModeId == dto.TransactionModeId);
        //        if (transactionMode == null)
        //        {
        //            transactionMode = new TransactionMode();
        //            TransactionMode transactionModeData = _mapper.Map<TransactionModeVM, TransactionMode>(dto);
        //            _context.TransactionMode.Add(transactionModeData);
        //        }
        //        else
        //        {
        //            _context.Entry(transactionMode).CurrentValues.SetValues(dto);
        //        }
        //        await _context.SaveChangesAsync();
        //        return dto;
        //    }
        //    return dto;
        //}

        public async Task Delete(int id)
        {
            if (id > 0)
            {
                TransactionMode transactionMode = _context.TransactionMode.Find(id);
                if (transactionMode != null)
                {
                    _context.TransactionMode.Remove(transactionMode);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<TransactionModeVM>> GetTransactionMode()
        {
            List<TransactionMode> transactionMode = await _context.TransactionMode.ToListAsync();

            List<TransactionModeVM> data = _mapper.Map<List<TransactionMode>, List<TransactionModeVM>>(transactionMode);

            return data;
        }

        public async Task<TransactionModeVM> GetTransactionModeById(int id)
        {
            TransactionMode transactionMode = await _context
                 .TransactionMode
                 .SingleOrDefaultAsync(x => x.TransactionModeId == id);
            return _mapper.Map<TransactionMode, TransactionModeVM>(transactionMode);
        }


    }
}
