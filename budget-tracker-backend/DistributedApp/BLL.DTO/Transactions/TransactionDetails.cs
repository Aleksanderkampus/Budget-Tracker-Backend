using Domain.Base;

namespace BLL.DTO.Transactions;

public class TransactionDetails : DomainIdentityId
{
    public string SenderReceiver { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Time { get; set; }
    
    public Guid CurrencyId { get; set; }
    public BLL.DTO.Currency? Currency { get; set; }
    
    public Guid AccountId { get; set; }

    public ICollection<BLL.DTO.Transactions.TransactionCategory>? CategoryTransactions { get; set; }
}