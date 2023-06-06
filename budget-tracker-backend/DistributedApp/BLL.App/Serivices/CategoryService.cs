using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Categories;
using DAL.Contracts.App;

using CategoryDetails = BLL.DTO.Categories.CategoryDetails;
using CategoryMapper = BLL.App.Mappers.CategoryMapper;
using CategoryWithTransactionAndBudget = BLL.DTO.Categories.CategoryWithTransactionAndBudget;


namespace BLL.App.Serivices;

public class CategoryService : BaseEntityService<DTO.Categories.SimpleCategory, Domain.FinancialCategory , IFinancialCategoryRepository>, ICategoryService
{
    protected IAppUOW Uow;
    private readonly CategoryMapper _mapper;
    public CategoryService(IAppUOW uow, CategoryMapper mapper) 
        : base(uow.FinancialCategoryRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DTO.Categories.SimpleCategory>> AllSimpleCategoriesAsync()
    {
        return (await Uow.FinancialCategoryRepository.AllSimpleCategoriesAsync())
            .Select(e => _mapper.MapSimpleCategory(e)).ToList()!;
    }

    public async Task<IEnumerable<CategoryWithTransactionAndBudget>> AllCategoryWithTransactionAndBudgetAsync(Guid userId)
    {
        return (await Uow.FinancialCategoryRepository.AllCategoryWithTransactionAndBudgetAsync(userId))
            .Select((e) => _mapper.MapCategoryWithTransactionAndBudget(e)).ToList();
    }

    public async Task<CategoryDetails> GetCategoryDetails(Guid userId, Guid id)
    {
        return _mapper.MapCategoryDetails(await Uow.FinancialCategoryRepository.GetCategoryDetails(userId, id));
    }

    public async Task<IEnumerable<PieChartData>> GetPieChartData(Guid userId)
    {
        return (await Uow.FinancialCategoryRepository.GetPieChartData(userId))
            .Select(e => _mapper.MapPieChart(e))
            .ToList();
    }
}