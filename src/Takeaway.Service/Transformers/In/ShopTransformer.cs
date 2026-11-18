using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Service.Transformers.In
{

  
    [Transformer]
    public class ShopTransformer: ITransformerMapper
    {
       
        public void Initialise()
        {
            Mapper.CreateMap<WsShop, ShopInfo>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ShopName))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.ShopMinOrder, opt => opt.MapFrom(s => s.MinOrder))
               .ForMember(d => d.DeliveryCharge, opt => opt.MapFrom(s => s.DeliveryCharge))
               .ForMember(d => d.ShopNumber, opt => opt.MapFrom(s => s.ShopNumber))
               .ForMember(d => d.ShopWeb, opt => opt.MapFrom(s => s.ShopUrl))
               .ForMember(d => d.ShopMobile, opt => opt.MapFrom(s => s.ShopMobile))
               .ForMember(d => d.ShopImage, opt => opt.MapFrom(s => s.ShopBackGroundImagePath))
               .ForMember(d => d.Address1, opt => opt.MapFrom(s => s.ShopAddress1))
               .ForMember(d => d.Address2, opt => opt.MapFrom(s => s.ShopAddress2))
               .ForMember(d => d.SpecialOffer, opt => opt.MapFrom(s => s.ShopSpecialOffer))
               .ForMember(d => d.ShopMobile, opt => opt.MapFrom(s => s.ShopMobile))
               .ForMember(d => d.ShopOpenHours, opt => opt.MapFrom(s => s.ShopOpenHours))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
               .ForMember(d => d.IsOpen, opt => opt.MapFrom(s => s.IsOpen))
               .ForMember(d => d.IsOpenByOwner, opt => opt.MapFrom(s => s.IsOpenByOwner))
               .ForMember(d => d.TakePayment, opt => opt.MapFrom(s => s.TakePayment))
               .ForMember(d => d.ShopDeliveryOpenHours, opt => opt.MapFrom(s => s.ShopDeliveryOpenHours))
               .ForMember(d => d.GoogleMap, opt => opt.MapFrom(s => s.GoogleMap))
               .ForMember(d => d.Androidlink, opt => opt.MapFrom(s => s.Androidlink))
               .ForMember(d => d.IoSlink, opt => opt.MapFrom(s => s.IoSlink))
               .ForMember(d => d.Isactive, opt => opt.MapFrom(s => s.Isactive))
               .ForMember(d => d.ActiveSync, opt => opt.MapFrom(s => s.ActiveSync))
               .ForMember(d => d.SmsBalance, opt => opt.MapFrom(s => s.SmsBalance))
               .ForMember(d => d.RegisterDiscount, opt => opt.MapFrom(s => s.RegisterDiscount))
               .ForMember(d => d.CollectionOptions, opt => opt.MapFrom(s => s.CollectionOptions))
               .ForMember(d => d.RequireTestOrder, opt => opt.MapFrom(s => s.RequireTestOrder))
               .ForMember(d => d.ShopNews, opt => opt.MapFrom(s => s.ShopNews));
        }
    }
}