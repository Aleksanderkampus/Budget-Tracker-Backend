using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Categories;

public class CategoryDetails
{
    [MaxLength(24)]
    public string Name { get; set; } = default!;

    [MaxLength(16)]
    public string HexColor { get; set; } = default!;

    public byte[] Icon { get; set; } = default!;
    
    public double AmountSpent { get; set; }
    
    public ICollection<BLL.DTO.Categories.GraphData>? GraphData { get; set; }
    
    public ICollection<BLL.DTO.Categories.TransactionDateGroup>? CategoryTransactionsDateGroups { get; set; }
}