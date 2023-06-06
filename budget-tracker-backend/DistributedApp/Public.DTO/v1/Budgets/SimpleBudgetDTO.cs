using Domain.Base;

namespace Public.DTO.v1;

public class SimpleBudgetDTO : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }
    
    public double AmountSpent { get; set; }
    
    public string CurrencySymbol { get; set; } = default!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public ICollection<SimpleCategoryDTO>? SimpleBudgetCategories { get; set; }
}