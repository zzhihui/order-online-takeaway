using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Service.Transformers.In
{




    [Transformer]
    public class MenuItemCategoryTransformer : ITransformerMapper
    {
     
        public void Initialise()
        {
            Mapper.CreateMap<WsMenuItemCategory, MenuItemCategory>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.WebImageUrl))
                .ForMember(d => d.Isactive, opt => opt.MapFrom(s => s.Isactive))
                .ForMember(d => d.IsInternal, opt => opt.MapFrom(s => s.IsInternal))
                .ForMember(d => d.CategoryGroupId, opt => opt.MapFrom(s => s.CategoryGroupId))
                .ForMember(d => d.SortOrder, opt => opt.MapFrom(s => s.SortOrder))
                .ForMember(d => d.TranslateName, opt => opt.MapFrom(s => s.TranslateName))
                ;
        }
    }
}