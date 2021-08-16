using AccountsReceivable.API.Models;
using AccountsReceivable.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Services.Interface
{
    public interface IDepositWalletAmountService 
    {
        Task<DepositWalletAmountVM> AddUpdateDepositWalletAmount(DepositWalletAmountVM dto);
       
    }
}
