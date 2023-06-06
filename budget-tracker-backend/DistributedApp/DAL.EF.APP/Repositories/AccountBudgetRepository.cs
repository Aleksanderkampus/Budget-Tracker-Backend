using DAL.Contracts.App;
using DAL.EF.BASE;
using Domain;

namespace DAL.EF.APP.Repositories;

public class AccountBudgetRepository : EFBaseRepository<AccountBudget, AppDbContext>, IAccountBudgetRepository
{
    public AccountBudgetRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}