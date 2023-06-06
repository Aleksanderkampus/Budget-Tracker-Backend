using System.ComponentModel.DataAnnotations;

namespace DAL.DTO;

public class SimpleCurrency
{
    public Guid Id { get; set; }
    
    [MaxLength(28)]
    public string Name { get; set; } = default!;
    
    [MaxLength(3)]
    public string Abbreviation { get; set; } = default!;

    [MaxLength(1)] public string Symbol { get; set; } = default!;
}