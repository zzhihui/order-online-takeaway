using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Settings;
using Takeaway.Service.Contract.Entities.Settings;

namespace Takeaway.Service.Transformers.In
{
  

    [Transformer]
    public class OfflineSettingTransformer : ITransformerMapper
    {
      
        public void Initialise()
        {
            Mapper.CreateMap<WsOfflineSetting, OfflineSetting>()
              .IgnoreAllUnmapped()
              .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
              .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
              .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
              .ForMember(d => d.SettingValue, opt => opt.MapFrom(s => s.SettingValue))
              ;
        }
    }
}