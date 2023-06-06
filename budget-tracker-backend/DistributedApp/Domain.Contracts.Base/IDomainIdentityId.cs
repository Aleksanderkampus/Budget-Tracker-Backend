namespace Domain.Contracts.Base;

public interface IDomainIdentityId<TKey>
    where TKey : struct , IEquatable<TKey>
{
    TKey Id { get; set; }
}

public interface IDomainIdentityId : IDomainIdentityId<Guid>
{
    
}