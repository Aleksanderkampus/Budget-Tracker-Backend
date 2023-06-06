using Domain.Base;

namespace BLL.DTO;

public class Currency : DomainIdentityId
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Abbreviation { get; set; } = default!;

    public string Symbol { get; set; } = default!;
}