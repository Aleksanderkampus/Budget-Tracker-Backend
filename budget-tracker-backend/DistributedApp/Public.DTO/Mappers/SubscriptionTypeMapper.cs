using AutoMapper;
using DAL.BASE;

namespace Public.DTO.Mappers;

public class SubscriptionTypeMapper : BaseMapper<BLL.DTO.SubscriptionType, Public.DTO.v1.SubscriptionType>
{
    public SubscriptionTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
}