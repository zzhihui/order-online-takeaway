using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Transformers.In
{


  
    [Transformer]
    public class TableCommandTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsTableCommand, TableCommand>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.ItemName, opt => opt.MapFrom(s => s.ItemName))
               .ForMember(d => d.ItemDescription, opt => opt.MapFrom(s => s.ItemDescription))
               .ForMember(d => d.TranslateName, opt => opt.MapFrom(s => s.TranslateName))
               .ForMember(d => d.Isactive, opt => opt.MapFrom(s => s.Isactive))
               .ForMember(d => d.SortOrder, opt => opt.MapFrom(s => s.SortOrder))
               ;
        }
    }
}