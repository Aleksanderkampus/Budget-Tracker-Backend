using AutoMapper;
using BLL.DTO;
using DAL.BASE;

public class SubscriptionMapper : BaseMapper<BLL.DTO.SubscriptionDetails, Domain.Subscription>
{
    public SubscriptionMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.SimpleSubscription? MapSimple(DAL.DTO.SimpleSubscription entity)
    {
        var res = Mapper.Map<BLL.DTO.SimpleSubscription>(entity);
        return res;
    }

    public BLL.DTO.SubscriptionDetails? MapSubscriptionDetails(DAL.DTO.SubscriptionDetails entity)
    {
        var res = Mapper.Map<BLL.DTO.SubscriptionDetails>(entity);
        return res;
    }
}