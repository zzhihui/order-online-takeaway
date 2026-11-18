using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Service.Transformers.In
{

   
    [Transformer]
    public class OrderTransformer : ITransformerMapper
    {
       

        public void Initialise()
        {
            Mapper.CreateMap<WsOrder, Order>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.OrderTotal, opt => opt.MapFrom(s => s.OrderTotal))
               .ForMember(d => d.OrderOriginalTotal, opt => opt.MapFrom(s => s.OrderOriginalTotal))
               .ForMember(d => d.ContactName, opt => opt.MapFrom(s => s.ContactName))
               .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
               .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
               .ForMember(d => d.DiscountRules, opt => opt.MapFrom(s => s.DiscountRules))
               .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Comment))
               .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.CustomerId))
               .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.StatusId))
               .ForMember(d => d.OrderItems, opt => opt.MapFrom(s => s.OrderItems))
               .ForMember(d => d.IsInternalOrder, opt => opt.MapFrom(s => s.IsInternalOrder))
               .ForMember(d => d.DeliveryOptionId, opt => opt.MapFrom(s => s.DeliveryType.Id))
               .ForMember(d => d.DeliveryCharge, opt => opt.MapFrom(s => s.DeliveryCharge))
               .ForMember(d => d.PaymentType, opt => opt.MapFrom(s => s.PaymentType))
               .ForMember(d => d.OrderRef, opt => opt.MapFrom(s => s.OrderRef))
               .ForMember(d => d.PaymentRef, opt => opt.MapFrom(s => s.PaymentRef))
               .ForMember(d => d.DriverName, opt => opt.MapFrom(s => s.DriverName))
               .ForMember(d => d.AgentId, opt => opt.MapFrom(s => s.AgentId))
               .ForMember(d => d.IsMobileOrder, opt => opt.MapFrom(s => s.IsMobileOrder))
               .ForMember(d => d.TableNumber, opt => opt.MapFrom(s => s.TableNumber))
               .ForMember(d => d.NumberOfPeople, opt => opt.MapFrom(s => s.NumberOfPeople))
               .ForMember(d => d.IsWalkIn, opt => opt.MapFrom(s => s.IsWalkIn))
               .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
               .ForMember(d => d.CancellationReason, opt => opt.MapFrom(s => s.CancellationReason))
               .ForMember(d => d.OrderSource, opt => opt.MapFrom(s => s.OrderSource))
               .ForMember(d => d.MarketingPermission, opt => opt.MapFrom(s => s.MarketingPermission))
               .ForMember(d => d.CollectionTime, opt => opt.MapFrom(s => s.CollectionTime))
               .ForMember(d => d.Guid, opt => opt.MapFrom(s => s.Guid))
               ;



            Mapper.CreateMap<WsMenuItem, OrderItem>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.MenuItemId, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Comment))
               .ForMember(d => d.DiscountPrice, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.OrderItemsSelections, opt => opt.MapFrom(s => s.SubItemSelections))
               ;
            //http://stackoverflow.com/questions/3145062/using-automapper-to-unflatten-a-dto

            Mapper.CreateMap<WsMenuSubItem, OrderItemsSelection>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.OrderItemSelectedId, opt => opt.MapFrom(s => s.Selected.Id))
               ;
        }
    }
}