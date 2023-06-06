namespace BLL.DTO.Categories;

public class TransactionDateGroup
{
    public DateTime Date { get; set; }
    
    public ICollection<BLL.DTO.Categories.CategoryTransactionDetails>? CategoryTransactions { get; set; }
}