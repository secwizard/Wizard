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
            CreateMap<OrderPaymentVM, OrderPayment>().ReverseMap();
            CreateMap<CustomerWalletRequest, CustomerWallet>().ReverseMap();
            CreateMap<OrderPaymentRequest, OrderPayment>().ReverseMap();
            CreateMap<TransactionModeRequest, TransactionMode>().ReverseMap();
            CreateMap<CashbackMasterRequest, CashBackMaster>().ReverseMap();
            CreateMap<UpdateTransaction, CustomerWallet>().ReverseMap();
            CreateMap<OrderPaymentRequest, CustomerWallet>().ReverseMap();
            CreateMap<OrderWithOutPaymentRequest, CustomerWallet>().ReverseMap();
            CreateMap<OrderWithOutPaymentRequest, OrderPayment>().ReverseMap();
        }
    }
}
