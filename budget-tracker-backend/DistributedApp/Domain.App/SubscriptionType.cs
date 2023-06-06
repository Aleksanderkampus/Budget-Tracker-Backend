using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class SubscriptionType : DomainIdentityId
{
    [MaxLength(20)]
    public string Name { get; set; } = default!;

    public ICollection<Subscription>? Subscriptions { get; set; }
}