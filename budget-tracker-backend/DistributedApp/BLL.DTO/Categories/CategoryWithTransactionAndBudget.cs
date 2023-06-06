namespace BLL.DTO.Categories;

public class CategoryWithTransactionAndBudget : SimpleCategory
{
    public int NumberOfTransactions { get; set; }
    public double AmountSpent { get; set; }
    public int NumberOfBudgets { get; set; }
}