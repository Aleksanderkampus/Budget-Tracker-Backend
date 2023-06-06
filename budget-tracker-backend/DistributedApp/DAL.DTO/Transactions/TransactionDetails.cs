using Domain;
using Domain.Base;

namespace DAL.DTO;

public class TransactionDetails : DomainIdentityId
{
    public string SenderReceiver { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Time { get; set; }
    
    public Guid CurrencyId { get; set; }
    public SimpleCurrency? Currency { get; set; }
    
    public Guid AccountId { get; set; }

    public ICollection<TransactionCategory>? CategoryTransactions { get; set; }
}