using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Service.Transformers.In
{

	
    [Transformer]
    public class MenuItemTransformer : ITransformerMapper
    {
	
        public void Initialise()
        {
            Mapper.CreateMap<WsMenuItem, MenuItem>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.ItemName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.TranslateName, opt => opt.MapFrom(s => s.TranslatedName))
                .ForMember(d => d.ItemDescription, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.ExtraComment, opt => opt.MapFrom(s => s.ExtraComment))
                .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.ItemPic, opt => opt.MapFrom(s => s.ImageUrl))
                .ForMember(d => d.VideoLink, opt => opt.MapFrom(s => s.VideoLink))
                .ForMember(d => d.HasSubOrder, opt => opt.MapFrom(s => s.HasSubOrder))
                .ForMember(d => d.Isactive, opt => opt.MapFrom(s => s.Isactive))
                .ForMember(d => d.MenuItemSubItems, opt => opt.MapFrom(s => s.SubItemSelections))
                .ForMember(d => d.Ishot, opt => opt.MapFrom(s => s.Ishot))
                .ForMember(d => d.MenuItemCategoryId, opt => opt.MapFrom(s => s.MenuItemCategoryId))
                .ForMember(d => d.NoSelection, opt => opt.MapFrom(s => s.NoSelection))
                .ForMember(d => d.SortOrder, opt => opt.MapFrom(s => s.SortOrder))
                .ForMember(d => d.ExcludeOnline, opt => opt.MapFrom(s => s.ExcludeOnline))
                .ForMember(d => d.FoodCategoryId, opt => opt.MapFrom(s => s.FoodCategoryId))
                .ForMember(d => d.RewardPoints, opt => opt.MapFrom(s => s.RewardPoints))

                ;


            Mapper.CreateMap<WsMenuSubItem, MenuItemSubItem>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.MenuSubItem, opt => opt.MapFrom(s => s))
                ;

            Mapper.CreateMap<WsMenuSubItem, MenuSubItem>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
               .ForMember(d => d.MenuSubItemItems, opt => opt.MapFrom(s => s.Selections))
               ;
            
            Mapper.CreateMap<WsMenuItem, MenuSubItemItem>()
              .IgnoreAllUnmapped()
              .ForMember(d => d.MenuItem, opt => opt.MapFrom(s => s))
              ;
        }
    }
}

