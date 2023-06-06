using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Public.DTO.v1;

public class SimpleCategoryDTO : DomainIdentityId
{
    [MaxLength(24)]
    public string Name { get; set; } = default!;

    [MaxLength(16)]
    public string HexColor { get; set; } = default!;
    
    public byte[]? Icon { get; set; }
}