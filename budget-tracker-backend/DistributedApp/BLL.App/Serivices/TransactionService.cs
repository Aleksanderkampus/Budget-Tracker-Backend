using BLL.App.Mappers;
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Transactions;
using DAL.Contracts.App;

using CategoryMapper = BLL.App.Mappers.CategoryMapper;


namespace BLL.App.Serivices;

public class TransactionService : BaseEntityService<BLL.DTO.Transactions.TransactionDetails, Domain.Transaction , ITransactionRepository>, ITransactionService
{
    protected IAppUOW Uow;
    private readonly TransactionMapper _mapper;
    
    public TransactionService(IAppUOW uow, TransactionMapper mapper) 
        : base(uow.TransactionRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }


    public async Task<IEnumerable<TransactionWithCategories?>> GetTransactionsWithCategories(Guid userId)
    {
        return (await Uow.TransactionRepository.GetTransactionsWithCategories(userId))
            .Select(e => _mapper.MapTransactionWithCategories(e!)).ToList();
    }

    public async Task<TransactionDetails?> GetDetails(Guid userId, Guid transactionId)
    {
        return _mapper.MapDetails(await Uow.TransactionRepository.GetDetails(userId, transactionId));
    }

    public async Task<IEnumerable<TransactionGraphData>> GetGraphData(Guid userId)
    {
        return (await Uow.TransactionRepository.GetGraphData(userId)).Select((g) => _mapper.MapGraphData(g)).ToList()!;
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.TransactionRepository.IsOwnedByUserAsync(id, userId);
    }
    
}