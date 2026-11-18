using AutoMapper;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Admin.Util.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Util.Transformers.In
{

    public interface ICustomerTransformer: ITransformer<ICustomerTransformer>
    {
    }


    public class CustomerTransformer: TransformerBase, ICustomerTransformer
    {
        public CustomerTransformer()
        {

            Mapper.CreateMap<WsShopCustomer, Customer>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.ContactName))
                .ForMember(d => d.Address1, opt => opt.MapFrom(s => s.Address))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Phone1, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.DeliveryCharge, opt => opt.MapFrom(s => s.DeliveryCharge));

        }
    }
}