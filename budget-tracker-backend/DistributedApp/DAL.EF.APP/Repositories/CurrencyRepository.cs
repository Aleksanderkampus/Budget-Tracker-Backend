using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.BASE;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.APP.Repositories;

public class CurrencyRepository: EFBaseRepository<Currency, AppDbContext>, ICurrencyRepository
{
    public CurrencyRepository(AppDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<SimpleCurrency>> AllSimpleCurrencyAsync()
    {
        return await RepositoryDbSet.Select((c) => new SimpleCurrency()
        {
            Id = c.Id,
            Name = c.Name,
            Abbreviation = c.Abbreviation,
            Symbol = c.Symbol
        }).AsNoTracking().ToListAsync();
    }
}