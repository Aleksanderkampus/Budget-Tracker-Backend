using Contracts.Base;

namespace DAL.BASE;

public class BaseMapper<TSource, TOutput> : IMapper<TSource, TOutput>
{
    protected readonly AutoMapper.IMapper Mapper;
    
    public BaseMapper(AutoMapper.IMapper mapper)
    {
        Mapper = mapper;
    }

    public TOutput? Map(TSource? entity)
    {
        return Mapper.Map<TOutput>(entity);
    }

    public TSource? Map(TOutput? entity)
    {
        return Mapper.Map<TSource>(entity);
    }
}