using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface ISubscriptionTypeRepository: IBaseRepository<SubscriptionType>, ISubscriptionTypeRepositoryCustom<SubscriptionType>
{
    
}

public interface ISubscriptionTypeRepositoryCustom<TEntity>
{
    
}