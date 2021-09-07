using AccountsReceivable.API.Models;
using Microsoft.EntityFrameworkCore;
namespace AccountsReceivable.API.Data
{
    public class AccountReceivableDataContext:DbContext
    {
        public AccountReceivableDataContext(DbContextOptions<AccountReceivableDataContext> options) : base(options)
        {
            Database.SetCommandTimeout(9000);
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
        public DbSet<CustomerWalletInfo> CustomerWalletInfo { get; set; }
    }
}
