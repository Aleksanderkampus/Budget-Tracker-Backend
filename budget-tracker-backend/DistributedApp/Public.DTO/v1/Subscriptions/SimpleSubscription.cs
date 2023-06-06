using Domain.Base;

namespace Public.DTO.v1;

public class SimpleSubscription : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime DateStarted { get; set; } = DateTime.Today;

    public DateTime NextPayment { get; set; }

    public Guid SubscriptionTypeId { get; set; }
    public SubscriptionType? SubscriptionType { get; set; }
}