namespace Public.DTO.v1;

public class CategoryWithTransactionAndBudgetDTO : SimpleCategoryDTO
{
    public int NumberOfTransactions { get; set; }
    public double AmountSpent { get; set; }
    public int NumberOfBudgets { get; set; }
}