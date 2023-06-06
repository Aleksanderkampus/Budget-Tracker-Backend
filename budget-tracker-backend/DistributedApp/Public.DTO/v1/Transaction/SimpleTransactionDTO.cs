using Domain.Base;

namespace Public.DTO.v1;

public class SimpleTransactionDTO: DomainIdentityId
{
    public string SenderReceiver { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Time { get; set; }
    
    public string CurrencySymbol { get; set; } = default!;
}