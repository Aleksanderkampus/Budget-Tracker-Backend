using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

public class CategoryBudget : DomainIdentityId
{
    public Guid BudgetId { get; set; }
    public Budget? Budget { get; set; }
    
    [ForeignKey("FinancialCategory")] 
    public Guid CategoryId { get; set; }
    public FinancialCategory? FinancialCategory { get; set; }
}