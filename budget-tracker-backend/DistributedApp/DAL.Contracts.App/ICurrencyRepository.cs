using DAL.Contracts.Base;
using DAL.DTO;
using Domain;

namespace DAL.Contracts.App;

public interface ICurrencyRepository : IBaseRepository<Currency>, ICurrencyRepositoryCustom<Currency>
{
    Task<IEnumerable<SimpleCurrency>> AllSimpleCurrencyAsync();
}

public interface ICurrencyRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    
}
