using DAL.Contracts.App;
using DAL.EF.BASE;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.APP.Repositories;

public class AccountRepository : EFBaseRepository<Account, AppDbContext>, IAccountRepository
{
    public AccountRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<Account>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet.Where((e) => e.UserId == userId).ToListAsync();
    }
}