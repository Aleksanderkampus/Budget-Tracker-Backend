using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    ICategoryService CategoryService { get; }
    ITransactionService TransactionService { get; }
    
    IBudgetService BudgetService { get; }
    
    ICurrencyService CurrencyService { get; }
    
    ISubscriptionService SubscriptionService { get; }
    
    ISubscriptionTypeService SubscriptionTypeService { get; }
}