using Domain.Contracts.Base;

namespace DAL.Contracts.Base;

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
    where TEntity : class, IDomainIdentityId
{
}

public interface IBaseRepository<TEntity, in TKey>
    where TEntity : class, IDomainIdentityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    Task<IEnumerable<TEntity>> AllAsync();
    
    Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    
    Task<TEntity?> FindAsync(TKey id);

    TEntity Add(TEntity entity);

    TEntity Update(TEntity entity);

    TEntity Remove(TEntity entity);
    
    Task<TEntity?> RemoveAsync(TKey id);

    
}