using BLL.App.Mappers;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App.Serivices;

public class SubscriptionTypeService: BaseEntityService<DTO.SubscriptionType, Domain.SubscriptionType , ISubscriptionTypeRepository>, ISubscriptionTypeService
{
    protected IAppUOW Uow;
    private readonly SubscriptionTypeMapper _mapper;
    
    public SubscriptionTypeService(IAppUOW uow, SubscriptionTypeMapper mapper) 
        : base(uow.SubscriptionTypeRepository, mapper)
    {
        Uow = uow;
        _mapper = mapper;
    }

}