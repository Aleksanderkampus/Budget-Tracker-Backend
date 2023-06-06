namespace BLL.DTO.Budgets;

public class BudgetCategories
{
    public BLL.DTO.Categories.SimpleCategory? Category { get; set; }

    public double TotalAmount { get; set; }
}