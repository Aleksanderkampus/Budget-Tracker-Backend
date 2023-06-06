using AutoMapper;
using DAL.BASE;
using Domain;

namespace BLL.App.Mappers;

public class CurrencyMapper : BaseMapper<BLL.DTO.Currency, Domain.Currency>
{
    public CurrencyMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Currency? MapCurrency(DAL.DTO.SimpleCurrency entity)
    {
        var res = Mapper.Map<BLL.DTO.Currency>(entity);
        return res;
    }
}