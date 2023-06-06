using AutoMapper;
using DAL.BASE;
using DAL.DTO;
using Domain;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class BudgetMapper : BaseMapper<BLL.DTO.Budgets.BudgetToEdit, Public.DTO.v1.BudgetToEdit>
{
    public BudgetMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public SimpleBudgetDTO? MapSimpleBudget(BLL.DTO.Budgets.SimpleBudget entity)
    {
        var res = Mapper.Map<SimpleBudgetDTO>(entity);
        return res;
    }
    
    public BudgetDetailsDTO? MapBudgetDetails(BLL.DTO.Budgets.BudgetDetails entity)
    {
        var res = Mapper.Map<BudgetDetailsDTO>(entity);
        return res;
    }
    
    public Public.DTO.v1.BudgetToEdit? MapBudgetToEdit(BLL.DTO.Budgets.BudgetToEdit entity)
    {
        var res = Mapper.Map<Public.DTO.v1.BudgetToEdit>(entity);
        return res;
    }
}