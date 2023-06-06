using AutoMapper;
using DAL.BASE;

namespace Public.DTO.Mappers;

public class SubscriptionMapper : BaseMapper<BLL.DTO.SubscriptionDetails, Public.DTO.v1.SubscriptionDetails>
{
    public SubscriptionMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public Public.DTO.v1.SimpleSubscription? MapSimple(BLL.DTO.SimpleSubscription entity)
    {
        var res = Mapper.Map<Public.DTO.v1.SimpleSubscription>(entity);
        return res;
    }
    
    public Public.DTO.v1.SubscriptionDetails? MapDetails(BLL.DTO.SubscriptionDetails entity)
    {
        var res = Mapper.Map<Public.DTO.v1.SubscriptionDetails>(entity);
        return res;
    }
}