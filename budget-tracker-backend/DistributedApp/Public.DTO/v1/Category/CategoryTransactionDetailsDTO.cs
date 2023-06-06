namespace Public.DTO.v1;

public class CategoryTransactionDetailsDTO
{
    public Guid TransactionId { get; set; }
    public Public.DTO.v1.TransactionWithCategories? Transaction { get; set; }
    
    public double Amount { get; set; }
}