using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Service.Transformers.In
{

    [Transformer]
    public class OptionItemTransformer : ITransformerMapper
    {
     
        public void Initialise()
        {
            Mapper.CreateMap<WsOptionItem, OptionItem>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ItemName, opt => opt.MapFrom(s => s.ItemName))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.TranslateName, opt => opt.MapFrom(s => s.TranslateName))
               .ForMember(d => d.ItemDescription, opt => opt.MapFrom(s => s.ItemDescription))
               .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice))
               .ForMember(d => d.Isactive, opt => opt.MapFrom(s => s.Isactive))
               .ForMember(d => d.GroupId, opt => opt.MapFrom(s => s.GroupId))
               .ForMember(d => d.SortOrder, opt => opt.MapFrom(s => s.SortOrder))
               ;
        }
    }
}

