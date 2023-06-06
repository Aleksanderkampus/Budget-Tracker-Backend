namespace DAL.DTO;

public class BudgetDetails
{
    public SimpleBudget? SimpleBudget { get; set; }

    public ICollection<BudgetCategories>? BudgetCategories { get; set; }
    
    public ICollection<TransactionDateGroup>? BudgetTransactions { get; set; }
}