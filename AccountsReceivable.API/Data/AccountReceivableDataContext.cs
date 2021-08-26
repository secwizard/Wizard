using AccountsReceivable.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace AccountsReceivable.API.Data
{
    public class AccountReceivableDataContext:DbContext
    {
        public AccountReceivableDataContext(DbContextOptions<AccountReceivableDataContext> options) : base(options)
        {
        }
        public DbSet<CustomerWallet> CustomerWallet { get; set; }
        public DbSet<CustomerWalletTransaction> CustomerWalletTransaction { get; set; }
        public DbSet<CustomerWalletTransactionDetail> CustomerWalletTransactionDetail { get; set; }
        public DbSet<OrderPayment> OrderPayment { get; set; }
        public DbSet<TransactionMode> TransactionMode { get; set; }
        public DbSet<DepositWalletAmount> DepositWalletAmount { get; set; }
        public DbSet<CashBackMasters> CashBackMaster { get; set; }
        public DbSet<CashBackTransaction> CashBackTransaction { get; set; }
        public DbSet<CashbackExclusion> CashbackExclusion { get; set; }
    }
}
