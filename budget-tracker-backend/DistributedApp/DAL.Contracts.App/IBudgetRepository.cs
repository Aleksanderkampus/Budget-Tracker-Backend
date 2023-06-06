using DAL.Contracts.Base;
using DAL.DTO;
using Domain;

namespace DAL.Contracts.App;

public interface IBudgetRepository : IBaseRepository<Budget>, IBudgetRepositoryCustom<Budget>
{
    Task<IEnumerable<SimpleBudget>> AllSimpleBudgetsAsync(Guid userId);  
    
    Task<BudgetDetails?> GetDetails(Guid userId,  Guid budgetId);
    
    Task<BudgetToEdit> GetBudgetToEdit(Guid userId, Guid budgetId);  
}

public interface IBudgetRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);

}
