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
    public class TablesTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsTables, Tables>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.Alias, opt => opt.MapFrom(s => s.Alias))
               .ForMember(d => d.NumberOfPeople, opt => opt.MapFrom(s => s.NumberOfPeople))
               .ForMember(d => d.Started, opt => opt.MapFrom(s => s.Started))
               .ForMember(d => d.Order, opt => opt.MapFrom(s => s.Order))
               ;

        }
    }
}