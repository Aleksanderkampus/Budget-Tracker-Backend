using Domain.Base;

namespace DAL.DTO;

public class SubscriptionDetails : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime DateStarted { get; set; }
    
    public Guid SubscriptionTypeId { get; set; }

    public Guid AccountId { get; set; }

    public Guid CurrencyId { get; set; }
    public DAL.DTO.SimpleCurrency? Currency { get; set; }
}