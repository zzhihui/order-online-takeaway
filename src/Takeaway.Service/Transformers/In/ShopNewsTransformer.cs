using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Transformers.In
{
  
    [Transformer]
    public class ShopNewsTransformer: ITransformerMapper
    {
        public void Initialise()
        {

            Mapper.CreateMap<WsShopNews, ShopNews>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.News, opt => opt.MapFrom(s => s.News))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated));
        }
    }
}