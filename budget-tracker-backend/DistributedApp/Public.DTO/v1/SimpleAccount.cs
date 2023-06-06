using Domain.Base;

namespace Public.DTO.v1;

public class SimpleAccount :DomainIdentityId
{
    public string Name { get; set; } = default!;

    public string Bank { get; set; } = default!;
}