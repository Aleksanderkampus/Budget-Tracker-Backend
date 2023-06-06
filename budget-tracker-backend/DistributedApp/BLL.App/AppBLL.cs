using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Serivices;
using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL : BaseBLL<IAppUOW>,IAppBLL
{
    
    protected IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private ICategoryService? _categoryService;
    private ITransactionService? _transactionService;
    private IBudgetService? _budgetService;
    private ICurrencyService? _currencyService;
    private ISubscriptionService? _subscriptionService;
    private ISubscriptionTypeService? _subscriptionTypeService;
    
    public ICategoryService CategoryService => 
        _categoryService ??= new CategoryService(Uow, new CategoryMapper(_mapper));
    
    public ITransactionService TransactionService => 
        _transactionService ??= new TransactionService(Uow, new TransactionMapper(_mapper));
    
    public IBudgetService BudgetService => 
        _budgetService ??= new BudgetService(Uow, new BudgetMapper(_mapper));
    
    public ICurrencyService CurrencyService => 
        _currencyService ??= new CurrencyService(Uow, new CurrencyMapper(_mapper));
    
    public ISubscriptionService SubscriptionService => 
        _subscriptionService ??= new SubscriptionService(Uow, new SubscriptionMapper(_mapper));
    
    public ISubscriptionTypeService SubscriptionTypeService => 
        _subscriptionTypeService ??= new SubscriptionTypeService(Uow, new SubscriptionTypeMapper(_mapper));
    
}