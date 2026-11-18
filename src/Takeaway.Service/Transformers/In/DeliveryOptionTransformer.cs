using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Transformers.In
{

   
    [Transformer]
    public class DeliveryOptionTransformer : ITransformerMapper
    {
       
        public void Initialise()
        {
            Mapper.CreateMap<WsDeliveryOption, DeliveryOption>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.IsInternal, opt => opt.MapFrom(s => s.IsInternal))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}