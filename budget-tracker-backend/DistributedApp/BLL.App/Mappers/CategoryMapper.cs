using AutoMapper;
using BLL.DTO;
using BLL.DTO.Categories;
using DAL.BASE;
using Public.DTO.v1;

namespace BLL.App.Mappers;

public class CategoryMapper : BaseMapper<SimpleCategory, Domain.FinancialCategory>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Categories.SimpleCategory? MapSimpleCategory(DAL.DTO.SimpleCategory entity)
    {
        var res = Mapper.Map<SimpleCategory>(entity);
        return res;
    }

    public BLL.DTO.Categories.CategoryWithTransactionAndBudget MapCategoryWithTransactionAndBudget(
        DAL.DTO.CategoryWithTransactionAndBudget entity)
    {
        var res = Mapper.Map<BLL.DTO.Categories.CategoryWithTransactionAndBudget>(entity);
        return res;
    }
    
    public BLL.DTO.Categories.CategoryDetails MapCategoryDetails(
        DAL.DTO.CategoryDetails entity)
    {
        var res = Mapper.Map<BLL.DTO.Categories.CategoryDetails>(entity);
        return res;
    }
    
    public BLL.DTO.Categories.PieChartData MapPieChart(
        DAL.DTO.PieChartData entity)
    {
        var res = Mapper.Map<BLL.DTO.Categories.PieChartData>(entity);
        return res;
    }
}