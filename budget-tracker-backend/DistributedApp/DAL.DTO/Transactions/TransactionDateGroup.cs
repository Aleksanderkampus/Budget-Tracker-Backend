namespace DAL.DTO;

public class TransactionDateGroup
{
    public DateTime Date { get; set; }
    
    public ICollection<CategoryTransactionDetails>? CategoryTransactions { get; set; }
}