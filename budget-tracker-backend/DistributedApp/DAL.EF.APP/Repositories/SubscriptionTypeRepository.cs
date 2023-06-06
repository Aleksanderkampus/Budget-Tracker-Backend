using DAL.Contracts.App;
using DAL.EF.BASE;
using Domain;

namespace DAL.EF.APP.Repositories;

public class SubscriptionTypeRepository: EFBaseRepository<SubscriptionType, AppDbContext>, 
    ISubscriptionTypeRepository
{
    public SubscriptionTypeRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}