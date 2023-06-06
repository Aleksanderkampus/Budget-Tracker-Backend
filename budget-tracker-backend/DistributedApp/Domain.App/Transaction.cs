using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain;

public class Transaction : DomainIdentityId
{
    public string SenderReceiver { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Time { get; set; } = DateTime.Now.ToUniversalTime();
    
    public Guid CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
    public ICollection<CategoryTransaction>? CategoryTransactions { get; set; }
}