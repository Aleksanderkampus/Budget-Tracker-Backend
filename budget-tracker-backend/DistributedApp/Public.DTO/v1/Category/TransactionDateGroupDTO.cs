namespace Public.DTO.v1;

public class TransactionDateGroupDTO
{
    public DateTime Date { get; set; }
    
    public ICollection<CategoryTransactionDetailsDTO>? CategoryTransactions { get; set; }
}