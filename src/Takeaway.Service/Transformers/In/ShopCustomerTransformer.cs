using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Transformers.In
{
   
    [Transformer]
    public class ShopCustomerTransformer: ITransformerMapper
    {
       
        public void Initialise()
        {
            Mapper.CreateMap<WsShopCustomer, Customer>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.ContactName))
               .ForMember(d => d.Phone1, opt => opt.MapFrom(s => s.PhoneNumber))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
               .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
               .ForMember(d => d.AuthenticationCode, opt => opt.MapFrom(s => s.AuthenticationCode))
               .ForMember(d => d.DeliveryCharge, opt => opt.MapFrom(s => s.DeliveryCharge))
               .ForMember(d => d.ErrorMessage, opt => opt.MapFrom(s => s.ErrorMessage))
               .ForMember(d => d.ValidationPending, opt => opt.MapFrom(s => s.ValidationPending))
               .ForMember(d => d.MarketingPermission, opt => opt.MapFrom(s => s.MarketingPermission))
               .ForMember(d => d.RewardPoints, opt => opt.MapFrom(s => s.RewardPoints))
               .ForMember(d => d.IsFirstPurchase, opt => opt.MapFrom(s => s.IsFirstPurchase))

               .ForMember(d => d.Address1, opt => opt.MapFrom(s => s.Address));
        }
    }
}