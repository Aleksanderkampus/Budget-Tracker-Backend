using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.Identity;

namespace Domain;

public class Account : DomainIdentityId
{
    [MaxLength(20)]
    public string Name { get; set; } = default!;

    [MaxLength(20)]
    public string Bank { get; set; } = default!;

    public Guid UserId { get; set; } = default!;
    public ApplicationUser? User { get; set; }

    public ICollection<Transaction>? Transaction { get; set; }
    
    public ICollection<AccountBudget>? AccountBudgets { get; set; }

    public ICollection<Subscription>? Subscriptions { get; set; }
}