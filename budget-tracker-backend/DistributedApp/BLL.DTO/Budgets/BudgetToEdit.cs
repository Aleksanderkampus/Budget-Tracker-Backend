using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO.Budgets;

public class BudgetToEdit : DomainIdentityId
{
        [MaxLength(100)]
        [MinLength(1)]
    public string Name { get; set; } = default!;

    public double AmountToSave { get; set; }
    
    public Guid CurrencyId { get; set; }

    public BLL.DTO.Currency? Currency { get; set; }

    public ICollection<BLL.DTO.SimpleAccount>? Account { get; set; } 
    
    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public ICollection<BLL.DTO.Categories.SimpleCategory>? SimpleBudgetCategories { get; set; }
}