using DAL.Contracts.App;
using DAL.EF.BASE;
using Domain;

namespace DAL.EF.APP.Repositories;

public class CategoryBudgetRepository : EFBaseRepository<CategoryBudget, AppDbContext>, ICategoryBudgetRepository
{
    public CategoryBudgetRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}