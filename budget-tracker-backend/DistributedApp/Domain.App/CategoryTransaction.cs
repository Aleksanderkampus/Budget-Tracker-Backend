using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain;

public class CategoryTransaction : DomainIdentityId
{
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    
    [ForeignKey("FinancialCategory")] 
    public Guid CategoryId { get; set; }
   
    public FinancialCategory? FinancialCategory { get; set; }

    public double Amount { get; set; }
}