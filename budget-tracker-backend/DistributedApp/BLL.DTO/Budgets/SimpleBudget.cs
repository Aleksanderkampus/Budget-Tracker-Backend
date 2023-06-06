using Domain.Base;

namespace BLL.DTO.Budgets;

public class SimpleBudget : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }
    
    public double AmountSpent { get; set; }
    
    public string CurrencySymbol { get; set; } = default!;


    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public ICollection<BLL.DTO.Categories.SimpleCategory>? SimpleBudgetCategories { get; set; }
}