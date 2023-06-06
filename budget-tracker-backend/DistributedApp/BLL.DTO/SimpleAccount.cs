using Domain.Base;

namespace BLL.DTO;

public class SimpleAccount : DomainIdentityId
{
    public string Name { get; set; } = default!;

    public string Bank { get; set; } = default!;
}