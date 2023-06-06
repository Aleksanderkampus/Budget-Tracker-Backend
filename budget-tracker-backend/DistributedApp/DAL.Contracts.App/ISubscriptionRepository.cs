using DAL.Contracts.Base;
using DAL.DTO;
using Domain;

namespace DAL.Contracts.App;

public interface ISubscriptionRepository: IBaseRepository<Subscription>, ITransactionRepositoryCustom<Subscription>
{
    Task<IEnumerable<SimpleSubscription>> AllSimpleAsync(Guid userId);
    
    Task<DAL.DTO.SubscriptionDetails> GetSubscriptionDetails(Guid userId, Guid id); 
}


public interface ISubscriptionRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}