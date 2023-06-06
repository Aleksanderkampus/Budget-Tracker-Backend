using BLL.App.Mappers;
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Budgets;
using BLL.DTO.Transactions;
using DAL.Contracts.App;
namespace BLL.App.Serivices;

public class BudgetService :
    BaseEntityService<BLL.DTO.Budgets.BudgetToEdit, Domain.Budget , IBudgetRepository>, IBudgetService
{
    protected IAppUOW Uow;
    private readonly BudgetMapper _mapper;
    
    public BudgetService(IAppUOW uow, BudgetMapper mapper) 
        : base(uow.BudgetRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }


    public async Task<IEnumerable<SimpleBudget>> AllSimpleBudgetsAsync(Guid userId)
    {
        return (await Uow.BudgetRepository.AllSimpleBudgetsAsync(userId)).Select((e) => _mapper.MapSimpleBudget(e))
            .ToList();
    }

    public async Task<BudgetDetails?> GetDetails(Guid userId, Guid budgetId)
    {
        return _mapper.MapBudgetDetails(await Uow.BudgetRepository.GetDetails(userId, budgetId));
    }

    public async Task<BudgetToEdit> GetBudgetToEdit(Guid userId, Guid budgetId)
    {
        return _mapper.MapBudgetToEdit(await Uow.BudgetRepository.GetBudgetToEdit(userId, budgetId))!;
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.BudgetRepository.IsOwnedByUserAsync(id, userId);
    }
}