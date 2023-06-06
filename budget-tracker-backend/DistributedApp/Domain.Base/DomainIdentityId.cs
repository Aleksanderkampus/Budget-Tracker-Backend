using Domain.Contracts.Base;

namespace Domain.Base;

public abstract class DomainIdentityId : DomainIdentityId<Guid>, IDomainIdentityId
{
}

public abstract class DomainIdentityId<TKey> : IDomainIdentityId<TKey>
where TKey : struct, IEquatable<TKey>
{
    public TKey Id { get; set; }
}