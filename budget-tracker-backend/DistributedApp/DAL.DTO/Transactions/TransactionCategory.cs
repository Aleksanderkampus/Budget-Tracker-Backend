using Domain.Base;

namespace DAL.DTO;

public class TransactionCategory : DomainIdentityId
{
    public Guid FinancialCategoryId { get; set; }
    public SimpleCategory? FinancialCategory { get; set; }

    public double Amount { get; set; }
}