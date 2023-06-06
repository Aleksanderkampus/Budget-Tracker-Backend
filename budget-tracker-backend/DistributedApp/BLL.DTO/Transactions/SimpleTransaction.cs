using Domain.Base;

namespace BLL.DTO.Transactions;

public class SimpleTransaction : DomainIdentityId
{
    public string SenderReceiver { get; set; } = default!;

    public double Amount { get; set; }

    public DateTime Time { get; set; }
    
    public string CurrencySymbol { get; set; } = default!;
}