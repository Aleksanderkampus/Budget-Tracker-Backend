using AutoMapper;
using DAL.BASE;

namespace BLL.App.Mappers;

public class SubscriptionTypeMapper: BaseMapper<BLL.DTO.SubscriptionType, Domain.SubscriptionType>
{
    public SubscriptionTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}