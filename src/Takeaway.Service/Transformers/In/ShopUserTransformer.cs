using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Authentication;


namespace Takeaway.Service.Transformers.In
{
  
    [Transformer]
    public class ShopUserTransformer : ITransformerMapper
    {

        public void Initialise()
        {
            Mapper.CreateMap<WsShopUser, ShopUser>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.ServerUrl, opt => opt.MapFrom(s => s.ServerUrl))
                .ForMember(d => d.AuthToken, opt => opt.MapFrom(s => s.AuthToken))
                .ForMember(d => d.UserGroupId, opt => opt.MapFrom(s => s.UserGroupId))
                .ForMember(d => d.Pin, opt => opt.MapFrom(s => s.Pin))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName));
        }
    }
}