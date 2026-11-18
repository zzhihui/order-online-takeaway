using AutoMapper;
using Core.Common.Infrastructure.AutoMapper;
using Takeaway.Admin.Util.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Util.Transformers.Out
{
    public interface IPhoneRecordTransformer : ITransformer<IPhoneRecordTransformer>
    {

    }

    public class PhoneRecordTransformer : TransformerBase, IPhoneRecordTransformer
    {
        public PhoneRecordTransformer()
        {
            Mapper.CreateMap<WsPhoneRecord, CallerId>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.LineId, opt => opt.MapFrom(s => s.Channel))
                .ForMember(d => d.Number, opt => opt.MapFrom(s => s.Phone))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))
                ;

            Mapper.AssertConfigurationIsValid();

        }

        



    }
}