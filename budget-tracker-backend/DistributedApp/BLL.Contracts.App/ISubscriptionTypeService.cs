using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ISubscriptionTypeService : IBaseRepository<BLL.DTO.SubscriptionType>, 
    ISubscriptionTypeRepositoryCustom<BLL.DTO.SubscriptionType>
{
    
}