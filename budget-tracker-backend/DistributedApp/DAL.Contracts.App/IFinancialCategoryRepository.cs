using DAL.Contracts.Base;
using DAL.DTO;
using Domain;

namespace DAL.Contracts.App;

public interface IFinancialCategoryRepository: IBaseRepository<FinancialCategory>, IFinancialCategoryRepositoryCustom<FinancialCategory>
{
    // add here custom methods for repo only
    Task<IEnumerable<SimpleCategory>> AllSimpleCategoriesAsync();
    Task<IEnumerable<CategoryWithTransactionAndBudget>> AllCategoryWithTransactionAndBudgetAsync(Guid userId);
    Task<CategoryDetails> GetCategoryDetails(Guid userId, Guid id);
    Task<IEnumerable<PieChartData>> GetPieChartData(Guid userId);

}


public interface IFinancialCategoryRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    
}
