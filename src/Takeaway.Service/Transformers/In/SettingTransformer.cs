using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Settings;
using Takeaway.Service.Contract.Entities.Settings;

namespace Takeaway.Service.Transformers.In
{


  
    [Transformer]
    public class SettingTransformer: ITransformerMapper
    {
    
        public void Initialise()
        {
            Mapper.CreateMap<WsSetting, ServerSetting>()
               .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.SettingKey, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.SettingValue, opt => opt.MapFrom(s => s.SettingValue))
               .ForMember(d => d.IsServer, opt => opt.MapFrom(s => s.IsServer))

               ;
        }
    }
}