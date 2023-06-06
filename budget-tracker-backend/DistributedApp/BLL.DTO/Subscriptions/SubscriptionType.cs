using Domain.Base;

namespace BLL.DTO;

public class SubscriptionType : DomainIdentityId
{
    public string Name { get; set; } = default!;
}