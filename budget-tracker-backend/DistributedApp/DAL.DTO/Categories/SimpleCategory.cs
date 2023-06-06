using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.DTO;

public class SimpleCategory : DomainIdentityId
{
    [MaxLength(24)]
    public string Name { get; set; } = default!;

    [MaxLength(16)]
    public string HexColor { get; set; } = default!;
    
    public byte[]? Icon { get; set; }
}