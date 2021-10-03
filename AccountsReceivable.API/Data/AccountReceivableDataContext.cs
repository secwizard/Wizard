using AccountsReceivable.API.Models;
using AccountsReceivable.API.Models.ResponseModel;
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
        public DbSet<CashBackMaster> CashBackMaster { get; set; }
        public DbSet<CashBackTransaction> CashBackTransaction { get; set; }
        public DbSet<CashbackExclusion> CashbackExclusion { get; set; }
        public DbSet<CustomerWalletInfo> CustomerWalletInfo { get; set; }
        public DbSet<CustomerWalletTransactionList> CustomerWalletTransactionList { get; set; }
        public DbSet<CustomerDepositAmount> CustomerDepositAmount { get; set; }
        public DbSet<OrderWithPayment> OrderWithPayment { get; set; }
        public DbSet<ResponseCheckCustomerWalletDetailForPlaceOrder> CheckCustomerWalletDetailForPlaceOrder { get; set; }
        public DbSet<GetCustomerCashBackList> GetCustomerCashBackList { get; set; }
        public DbSet<ResponseSaveCustomerCashBackDetail> SaveCustomerCashBackDetail { get; set; }

        public DbSet<ResponseSaveCustomerPayment> SaveCustomerPayment { get; set; }
        public DbSet<ResponseCustomerOrderPaymentList> CustomerOrderPaymentList { get; set; }
        public DbSet<ResponseGetTransactionMode> GetTransactionMode { get; set; }
    }
}
