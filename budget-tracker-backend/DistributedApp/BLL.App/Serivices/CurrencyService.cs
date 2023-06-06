using BLL.App.Mappers;
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using BLL.DTO.Transactions;
using DAL.Contracts.App;

using CategoryMapper = BLL.App.Mappers.CategoryMapper;


namespace BLL.App.Serivices;

public class CurrencyService : BaseEntityService<BLL.DTO.Currency, Domain.Currency , ICurrencyRepository>, ICurrencyService
{
    protected IAppUOW Uow;
    private readonly CurrencyMapper _mapper;
    
    public CurrencyService(IAppUOW uow, CurrencyMapper mapper) 
        : base(uow.CurrencyRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }


    public async Task<IEnumerable<Currency>> AllSimpleCurrencyAsync()
    {
        return (await Uow.CurrencyRepository.AllSimpleCurrencyAsync())
            .Select(c => _mapper.MapCurrency(c)).ToList();
    }
}