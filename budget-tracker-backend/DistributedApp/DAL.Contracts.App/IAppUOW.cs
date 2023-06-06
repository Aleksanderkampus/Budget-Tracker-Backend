using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    //list my repositories
    IAccountBudgetRepository AccountBudgetRepository { get; }
    IAccountRepository AccountRepository { get; }
    IBudgetRepository BudgetRepository { get; }
    ICategoryBudgetRepository CategoryBudgetRepository { get; }
    ICategoryTransactionRepository CategoryTransactionRepository { get; }
    ICurrencyRepository CurrencyRepository { get; }
    IFinancialCategoryRepository FinancialCategoryRepository { get; }
    ISubscriptionRepository SubscriptionRepository { get; }
    ISubscriptionTypeRepository SubscriptionTypeRepository { get; }
    ITransactionRepository TransactionRepository { get; }
}