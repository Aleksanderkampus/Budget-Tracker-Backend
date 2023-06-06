using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1;

public class CurrencyDTO
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Abbreviation { get; set; } = default!;

    public string Symbol { get; set; } = default!;
}