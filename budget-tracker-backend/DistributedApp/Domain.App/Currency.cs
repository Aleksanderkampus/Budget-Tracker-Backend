using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class Currency : DomainIdentityId
{
    [MaxLength(42)]
    public string Name { get; set; } = default!;

    [MaxLength(6)]
    public string Abbreviation { get; set; } = default!;

    [MaxLength(6)] public string Symbol { get; set; } = default!;

    public ICollection<Subscription>? Subscriptions { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }

    public ICollection<Budget>? Budgets { get; set; }
}