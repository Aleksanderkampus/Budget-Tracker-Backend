using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ICategoryService 
    : IBaseRepository<BLL.DTO.Categories.SimpleCategory>, 
        IFinancialCategoryRepositoryCustom<BLL.DTO.Categories.SimpleCategory>
{
    Task<IEnumerable<BLL.DTO.Categories.SimpleCategory>> AllSimpleCategoriesAsync();
    
    Task<IEnumerable<BLL.DTO.Categories.CategoryWithTransactionAndBudget>> AllCategoryWithTransactionAndBudgetAsync(Guid userId);
    
    Task<BLL.DTO.Categories.CategoryDetails> GetCategoryDetails(Guid userId, Guid id); 
    
    Task<IEnumerable<BLL.DTO.Categories.PieChartData>> GetPieChartData(Guid userId);
}