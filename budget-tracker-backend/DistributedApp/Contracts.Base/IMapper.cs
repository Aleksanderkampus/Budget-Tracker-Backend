namespace Contracts.Base;

public interface IMapper<TSource, TOutput>
{
    TOutput? Map(TSource? entity);
    TSource? Map(TOutput? entity);
}