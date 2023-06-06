namespace BLL.DTO.Categories;

public class CategoryTransactionDetails
{
    public Guid TransactionId { get; set; }
    public BLL.DTO.Transactions.TransactionWithCategories? Transaction { get; set; }
    
    public double Amount { get; set; }
}