using DAL.Contracts.Base;
using DAL.DTO;
using Domain;

namespace DAL.Contracts.App;

public interface ITransactionRepository: IBaseRepository<Transaction>, ITransactionRepositoryCustom<Transaction>
{
    Task<IEnumerable<DAL.DTO.TransactionWithCategories?>> GetTransactionsWithCategories(Guid userId);
    Task<TransactionDetails?> GetDetails(Guid userId,  Guid transactionId);
    
    Task<IEnumerable<TransactionGraphData>> GetGraphData(Guid userId);
}


public interface ITransactionRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}