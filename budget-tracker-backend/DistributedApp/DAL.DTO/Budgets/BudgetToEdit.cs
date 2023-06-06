using Domain.Base;

namespace DAL.DTO;

public class BudgetToEdit : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }
    
    public Guid CurrencyId { get; set; }
    
    public DAL.DTO.SimpleCurrency? Currency { get; set; } 

    public ICollection<DAL.DTO.SimpleAccount>? Account { get; set; } 
    
    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public ICollection<DAL.DTO.SimpleCategory>? SimpleBudgetCategories { get; set; }
}