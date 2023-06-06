namespace BLL.DTO.Transactions;

public class TransactionWithCategories
{
    public BLL.DTO.Transactions.SimpleTransaction? Transaction { get; set; }
    
    public ICollection<BLL.DTO.Categories.SimpleCategory>? Categories { get; set; }
}