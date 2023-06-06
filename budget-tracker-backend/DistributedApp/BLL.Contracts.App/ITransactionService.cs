using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITransactionService : IBaseRepository<BLL.DTO.Transactions.TransactionDetails>, 
    ITransactionRepositoryCustom<BLL.DTO.Transactions.TransactionDetails>
{
    Task<IEnumerable<BLL.DTO.Transactions.TransactionWithCategories?>> GetTransactionsWithCategories(Guid userId);
    Task<BLL.DTO.Transactions.TransactionDetails?> GetDetails(Guid userId,  Guid transactionId);
    
    Task<IEnumerable<BLL.DTO.Transactions.TransactionGraphData>> GetGraphData(Guid userId);
}