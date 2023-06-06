using AutoMapper;
using DAL.BASE;
using DAL.DTO;
using Domain;
using Public.DTO.v1;


namespace Public.DTO.Mappers;

public class CategoryMapper: BaseMapper<FinancialCategory, SimpleCategoryDTO>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public SimpleCategoryDTO? MapSimpleCategory(BLL.DTO.Categories.SimpleCategory entity)
    {
        var res = Mapper.Map<SimpleCategoryDTO>(entity);
        return res;
    }

    public CategoryWithTransactionAndBudgetDTO MapCategoryWithTransactionAndBudget(
        BLL.DTO.Categories.CategoryWithTransactionAndBudget entity)
    {
        var res = Mapper.Map<CategoryWithTransactionAndBudgetDTO>(entity);
        return res;
    }
    
    public CategoryDetailsDTO MapCategoryDetails(
        BLL.DTO.Categories.CategoryDetails entity)
    {
        var res = Mapper.Map<CategoryDetailsDTO>(entity);
        return res;
    }
    
    public Public.DTO.v1.PieChartData MapPieChart(
        BLL.DTO.Categories.PieChartData entity)
    {
        var res = Mapper.Map<Public.DTO.v1.PieChartData>(entity);
        return res;
    }
}