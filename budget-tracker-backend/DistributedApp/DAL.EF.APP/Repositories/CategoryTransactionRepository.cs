using DAL.Contracts.App;
using DAL.EF.BASE;
using Domain;

namespace DAL.EF.APP.Repositories;

public class CategoryTransactionRepository : EFBaseRepository<CategoryTransaction, AppDbContext>, 
    ICategoryTransactionRepository
{
    public CategoryTransactionRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}