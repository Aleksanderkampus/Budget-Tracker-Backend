namespace DAL.DTO;

public class CategoryTransactionDetails
{
    public Guid TransactionId { get; set; }
    public DAL.DTO.TransactionWithCategories? Transaction { get; set; }
    
    public double Amount { get; set; }
}