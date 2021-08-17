using AutoMapper;
using AccountsReceivable.API.Models;
using AccountsReceivable.API.ViewModels;
using AccountsReceivable.API.Models.RequestModel;

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
            CreateMap<DepositWalletAmount, CustomerWalletVM>().ReverseMap();
            CreateMap<CustomerWalletRequest, CustomerWallet>().ReverseMap();
            CreateMap<OrderPaymentRequest, OrderPayment>().ReverseMap();

        }
    }
}
