using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Public.DTO.v1;

public class CategoryDetailsDTO 
{
    [MaxLength(24)]
    public string Name { get; set; } = default!;

    [MaxLength(16)]
    public string HexColor { get; set; } = default!;

    public byte[] Icon { get; set; } = default!;
    
    public double AmountSpent { get; set; }
    
    public ICollection<GraphDataDTO>? GraphData { get; set; }
    
    public ICollection<TransactionDateGroupDTO>? CategoryTransactionsDateGroups { get; set; }
}