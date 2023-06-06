using Domain.Base;

namespace Public.DTO.v1;

public class SubscriptionDetails : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime DateStarted { get; set; } 
    
    public Guid SubscriptionTypeId { get; set; }

    public Guid AccountId { get; set; }

    public Guid CurrencyId { get; set; }
    public Public.DTO.v1.CurrencyDTO? Currency { get; set; }
}