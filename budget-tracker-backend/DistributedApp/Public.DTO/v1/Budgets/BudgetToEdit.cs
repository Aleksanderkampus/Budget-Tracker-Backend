using Domain.Base;

namespace Public.DTO.v1;

public class BudgetToEdit : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }

    public Guid CurrencyId { get; set; }
    
    public Public.DTO.v1.CurrencyDTO? Currency { get; set; }

    public ICollection<Public.DTO.v1.SimpleAccount>? Account { get; set; }
    
    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public ICollection<Public.DTO.v1.SimpleCategoryDTO>? SimpleBudgetCategories { get; set; }
}