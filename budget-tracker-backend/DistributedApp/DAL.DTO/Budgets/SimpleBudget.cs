using Domain.Base;

namespace DAL.DTO;

public class SimpleBudget : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }
    
    public double AmountSpent { get; set; }

    public string CurrencySymbol { get; set; } = default!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public ICollection<SimpleCategory>? SimpleBudgetCategories { get; set; }
}