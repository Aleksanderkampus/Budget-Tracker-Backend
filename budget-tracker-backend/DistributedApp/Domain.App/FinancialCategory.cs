using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain;

public class FinancialCategory : DomainIdentityId
{
    [MaxLength(24)]
    public string Name { get; set; } = default!;

    [MaxLength(16)]
    public string HexColor { get; set; } = default!;
    
    public byte[]? Icon { get; set; }
    public ICollection<CategoryTransaction>? CategoryTransactions { get; set; }
    
    public ICollection<CategoryBudget>? CategoryBudgets { get; set; }
}