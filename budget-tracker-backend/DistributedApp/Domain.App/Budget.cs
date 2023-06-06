using Domain.Base;

namespace Domain;

public class Budget : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public Guid CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    public ICollection<AccountBudget>? AccountBudgets { get; set; }
    
    public ICollection<CategoryBudget>? CategoryBudgets { get; set; }
}
