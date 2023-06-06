using AutoMapper;
using BLL.DTO.Budgets;
using DAL.BASE;
using Domain;

namespace BLL.App.Mappers;

public class BudgetMapper : BaseMapper<BLL.DTO.Budgets.BudgetToEdit, Domain.Budget>
{
    public BudgetMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Budgets.SimpleBudget? MapSimpleBudget(DAL.DTO.SimpleBudget entity)
    {
        var res = Mapper.Map<BLL.DTO.Budgets.SimpleBudget>(entity);
        return res;
    }
    
    public BLL.DTO.Budgets.BudgetDetails? MapBudgetDetails(DAL.DTO.BudgetDetails entity)
    {
        var res = Mapper.Map<BLL.DTO.Budgets.BudgetDetails>(entity);
        return res;
    }

    public BLL.DTO.Budgets.BudgetToEdit? MapBudgetToEdit(DAL.DTO.BudgetToEdit entity)
    {
        var res = Mapper.Map<BLL.DTO.Budgets.BudgetToEdit>(entity);
        return res;
    }
}