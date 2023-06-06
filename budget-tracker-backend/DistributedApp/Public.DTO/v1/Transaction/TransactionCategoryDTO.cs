using Domain;
using Domain.Base;

namespace Public.DTO.v1;

public class TransactionCategoryDTO : DomainIdentityId
{
    public Guid FinancialCategoryId { get; set; }
    public SimpleCategoryDTO? FinancialCategory { get; set; }

    public double Amount { get; set; }
}