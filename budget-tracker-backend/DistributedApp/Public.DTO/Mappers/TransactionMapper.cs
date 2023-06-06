using System.Transactions;
using AutoMapper;
using DAL.BASE;
using DAL.DTO;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class TransactionMapper : BaseMapper<BLL.DTO.Transactions.TransactionDetails, TransactionDetailsDTO>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public TransactionDetailsDTO? MapDetails(BLL.DTO.Transactions.TransactionDetails entity)
    {
        var res = Mapper.Map<TransactionDetailsDTO>(entity);
        return res;
    }
    
    public Public.DTO.v1.TransactionGraphData? MapGraphData(BLL.DTO.Transactions.TransactionGraphData entity)
    {
        var res = Mapper.Map<Public.DTO.v1.TransactionGraphData>(entity);
        return res;
    }
    
    public Public.DTO.v1.TransactionWithCategories? MapTransactionWithCategories(BLL.DTO.Transactions.TransactionWithCategories entity)
    {
        var res = Mapper.Map<Public.DTO.v1.TransactionWithCategories>(entity);
        return res;
    }
}