using Domain.Base;

namespace DAL.DTO;

public class TransactionWithCategories : DomainIdentityId
{
    public DAL.DTO.SimpleTransaction? Transaction { get; set; }
    
    public ICollection<DAL.DTO.SimpleCategory>? Categories { get; set; }
}