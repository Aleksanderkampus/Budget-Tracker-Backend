using Domain.Base;

namespace BLL.DTO.Transactions;

public class TransactionCategory : DomainIdentityId
{
    public Guid FinancialCategoryId { get; set; }
    public BLL.DTO.Categories.SimpleCategory? FinancialCategory { get; set; }

    public double Amount { get; set; }
}