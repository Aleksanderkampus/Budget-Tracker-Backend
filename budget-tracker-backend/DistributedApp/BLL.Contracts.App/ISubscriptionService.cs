using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ISubscriptionService : IBaseRepository<BLL.DTO.SubscriptionDetails>, 
    ITransactionRepositoryCustom<BLL.DTO.SubscriptionDetails>
{
    Task<IEnumerable<BLL.DTO.SimpleSubscription>> AllSimpleAsync(Guid userId);
    
    Task<BLL.DTO.SubscriptionDetails>  GetSubscriptionDetails(Guid userId, Guid id); 
}