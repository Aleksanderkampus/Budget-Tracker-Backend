using AutoMapper;
using DAL.BASE;
using DAL.DTO;
using Domain;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class CurrencyMapper : BaseMapper<Currency, CurrencyDTO>
{
    public CurrencyMapper(IMapper mapper) : base(mapper)
    {
    }

    public CurrencyDTO? MapSimpleCurrency(BLL.DTO.Currency entity)
    {
        var res = Mapper.Map<CurrencyDTO>(entity);
        return res;
    }
}