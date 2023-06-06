using Domain.Base;

namespace DAL.DTO;

public class BudgetCategories 
{
    public SimpleCategory? Category { get; set; }

    public double TotalAmount { get; set; }
}