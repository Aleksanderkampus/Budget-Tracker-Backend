namespace BLL.DTO.Budgets;

public class BudgetDetails
{
    public BLL.DTO.Budgets.SimpleBudget? SimpleBudget { get; set; }

    public ICollection<BudgetCategories>? BudgetCategories { get; set; }
    
    public ICollection<BLL.DTO.Categories.TransactionDateGroup>? BudgetTransactions { get; set; }
}