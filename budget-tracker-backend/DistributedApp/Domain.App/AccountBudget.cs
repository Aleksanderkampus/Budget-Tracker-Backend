using System.Reflection.Metadata;
using Domain.Base;

namespace Domain;

public class AccountBudget : DomainIdentityId
{
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
    
    public Guid BudgetId { get; set; }
    public Budget? Budget { get; set; }
}