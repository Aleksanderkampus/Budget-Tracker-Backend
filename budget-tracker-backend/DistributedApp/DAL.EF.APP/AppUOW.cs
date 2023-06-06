using DAL.Contracts.App;
using DAL.EF.APP.Repositories;
using DAL.EF.BASE;

namespace DAL.EF.APP;

public class AppUOW: EFBaseUOW<AppDbContext>, IAppUOW
{
    public AppUOW(AppDbContext datacontext) : base(datacontext)
    {
    }

    private IAccountBudgetRepository? _accountBudgetRepository;
    public IAccountBudgetRepository AccountBudgetRepository =>
        _accountBudgetRepository ??= new AccountBudgetRepository(UowDbContext);

    private IAccountRepository? _accountRepository;
    public IAccountRepository AccountRepository =>
        _accountRepository ??= new AccountRepository(UowDbContext);

    private IBudgetRepository? _budgetRepository;
    public IBudgetRepository BudgetRepository =>
        _budgetRepository ??= new BudgetRepository(UowDbContext);

    private ICategoryBudgetRepository? _categoryBudgetRepository;
    public ICategoryBudgetRepository CategoryBudgetRepository =>
        _categoryBudgetRepository ??= new CategoryBudgetRepository(UowDbContext);

    private ICategoryTransactionRepository? _categoryTransactionRepository;
    public ICategoryTransactionRepository CategoryTransactionRepository =>
        _categoryTransactionRepository ??= new CategoryTransactionRepository(UowDbContext);

    private ICurrencyRepository? _currencyRepository;
    public ICurrencyRepository CurrencyRepository =>
        _currencyRepository ??= new CurrencyRepository(UowDbContext);

    private IFinancialCategoryRepository? _financialCategoryRepository;
    public IFinancialCategoryRepository FinancialCategoryRepository =>
        _financialCategoryRepository ??= new FinancialCategoryRepository(UowDbContext);

    private ISubscriptionRepository? _subscriptionRepository;
    public ISubscriptionRepository SubscriptionRepository =>
        _subscriptionRepository ??= new SubscriptionRepository(UowDbContext);

    private ISubscriptionTypeRepository? _subscriptionTypeRepository;
    public ISubscriptionTypeRepository SubscriptionTypeRepository =>
        _subscriptionTypeRepository ??= new SubscriptionTypeRepository(UowDbContext);

    private ITransactionRepository? _transactionRepository;
    public ITransactionRepository TransactionRepository =>
        _transactionRepository ??= new TransactionRepository(UowDbContext);
}