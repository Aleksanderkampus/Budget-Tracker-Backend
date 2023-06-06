using Domain;
using Domain.Base;
using Domain.Contracts.Base;

namespace Public.DTO.v1;

public class TransactionDetailsDTO : DomainIdentityId
{
    public string SenderReceiver { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Time { get; set; }
    
    public Guid CurrencyId { get; set; }
    public CurrencyDTO? Currency { get; set; }
    
    public Guid AccountId { get; set; }

    public ICollection<TransactionCategoryDTO>? CategoryTransactions { get; set; }
}