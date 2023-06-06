using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using DAL.Contracts.App;


namespace BLL.App.Serivices;

public class SubscriptionService : BaseEntityService<DTO.SubscriptionDetails, Domain.Subscription , ISubscriptionRepository>, ISubscriptionService
{
    protected IAppUOW Uow;
    private readonly SubscriptionMapper _mapper;
    public SubscriptionService(IAppUOW uow, SubscriptionMapper mapper) 
        : base(uow.SubscriptionRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.SubscriptionRepository.IsOwnedByUserAsync(id, userId);
    }

    public async Task<IEnumerable<SimpleSubscription>> AllSimpleAsync(Guid userId)
    {
        return (await Uow.SubscriptionRepository.AllSimpleAsync(userId))
            .Select(e => _mapper.MapSimple(e)).ToList()!;
    }

    public async Task<SubscriptionDetails> GetSubscriptionDetails(Guid userId, Guid id)
    {
        return _mapper.MapSubscriptionDetails(await Uow.SubscriptionRepository.GetSubscriptionDetails(userId, id))!;
    }
}