using Domain.Base;

namespace DAL.DTO;

public class SimpleAccount : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public string Bank { get; set; } = default!;
}