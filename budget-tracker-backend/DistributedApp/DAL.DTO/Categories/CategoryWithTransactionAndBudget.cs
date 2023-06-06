namespace DAL.DTO;

public class CategoryWithTransactionAndBudget : SimpleCategory
{
    public int NumberOfTransactions { get; set; }
    public double AmountSpent { get; set; }
    public int NumberOfBudgets { get; set; }
}