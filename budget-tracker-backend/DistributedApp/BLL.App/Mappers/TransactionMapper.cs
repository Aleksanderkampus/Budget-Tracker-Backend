using AutoMapper;
using DAL.BASE;
using Domain;

namespace BLL.App.Mappers;

public class TransactionMapper : BaseMapper<BLL.DTO.Transactions.TransactionDetails, Domain.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Transactions.TransactionDetails? MapDetails(DAL.DTO.TransactionDetails entity)
    {
        var res = Mapper.Map<BLL.DTO.Transactions.TransactionDetails>(entity);
        return res;
    }
    
    public BLL.DTO.Transactions.TransactionGraphData? MapGraphData(DAL.DTO.TransactionGraphData entity)
    {
        var res = Mapper.Map<BLL.DTO.Transactions.TransactionGraphData>(entity);
        return res;
    }
    
    public BLL.DTO.Transactions.TransactionWithCategories? MapTransactionWithCategories(DAL.DTO.TransactionWithCategories entity)
    {
        var res = Mapper.Map<BLL.DTO.Transactions.TransactionWithCategories>(entity);
        return res;
    }
}