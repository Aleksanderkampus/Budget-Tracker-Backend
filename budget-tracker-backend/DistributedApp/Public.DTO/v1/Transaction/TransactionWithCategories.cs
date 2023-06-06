namespace Public.DTO.v1;

public class TransactionWithCategories
{
    public SimpleTransactionDTO? Transaction { get; set; }
    
    public ICollection<SimpleCategoryDTO>? Categories { get; set; }
}