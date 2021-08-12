using AutoMapper;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.ViewModels;
namespace AccountsReceivable.API.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CustomerWalletVM, CustomerWallet>().ReverseMap();
            CreateMap<CustomerWalletTransactionVM, CustomerWalletTransaction>().ReverseMap();
            CreateMap<CustomerWalletTransactionDetailVM, CustomerWalletTransactionDetail>().ReverseMap();
            CreateMap<OrderPaymentVM, OrderPayment>().ReverseMap();
            CreateMap<TransactionModeVM, TransactionMode>().ReverseMap();
        }
    }
}
