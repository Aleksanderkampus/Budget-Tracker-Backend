namespace Public.DTO.v1;

public class BudgetDetailsDTO
{
    public SimpleBudgetDTO? SimpleBudget { get; set; }

    public ICollection<BudgetCategoriesDTO>? BudgetCategories { get; set; }
    
    public ICollection<TransactionDateGroupDTO>? BudgetTransactions { get; set; }
}