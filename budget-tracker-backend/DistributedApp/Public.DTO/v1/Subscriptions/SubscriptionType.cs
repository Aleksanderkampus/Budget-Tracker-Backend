using Domain.Base;

namespace Public.DTO.v1;

public class SubscriptionType : DomainIdentityId
{
    public string Name { get; set; } = default!;
}