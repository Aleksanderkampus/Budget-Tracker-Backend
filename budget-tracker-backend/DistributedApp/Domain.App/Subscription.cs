using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class Subscription : DomainIdentityId
{
    [MaxLength(20)]
    public string Name { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Date { get; set; }

    public Guid AccountId { get; set; }
    public Account? Account { get; set; }

    public Guid CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    public Guid SubscriptionTypeId { get; set; }
    public SubscriptionType? SubscriptionType { get; set; }
}