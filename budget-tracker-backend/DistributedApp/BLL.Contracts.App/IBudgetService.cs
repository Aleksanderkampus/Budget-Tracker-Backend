using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IBudgetService : IBaseRepository<BLL.DTO.Budgets.BudgetToEdit>, 
    IBudgetRepositoryCustom<BLL.DTO.Budgets.BudgetToEdit>
{
    Task<IEnumerable<BLL.DTO.Budgets.SimpleBudget>> AllSimpleBudgetsAsync(Guid userId);  
    
    Task<BLL.DTO.Budgets.BudgetDetails?> GetDetails(Guid userId,  Guid budgetId);
    
    Task<BLL.DTO.Budgets.BudgetToEdit> GetBudgetToEdit(Guid userId, Guid budgetId);  
}