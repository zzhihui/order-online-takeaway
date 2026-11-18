using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Transformers.In
{

    [Transformer]
    public class DeliveryCostTransformer: ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsDeliveryCost, DeliveryCost>()
              .IgnoreAllUnmapped()
              .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
              .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
              .ForMember(d => d.DeliveryCostAmount, opt => opt.MapFrom(s => s.DeliveryCostAmount))
              .ForMember(d => d.FromDistance, opt => opt.MapFrom(s => s.FromDistance))
              .ForMember(d => d.ToDistance, opt => opt.MapFrom(s => s.ToDistance))
              .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
              ;
        }
    }
}