using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.BASE;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.APP.Repositories;

public class BudgetRepository : EFBaseRepository<Budget, AppDbContext>, IBudgetRepository
{
    public BudgetRepository(AppDbContext dataContext) : base(dataContext)
    {
    }


    public async Task<IEnumerable<SimpleBudget>> AllSimpleBudgetsAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(b => b.AccountBudgets!)
            .ThenInclude(ab => ab.Account)
            .Include(b => b.Currency)
            .Include(b => b.CategoryBudgets!)
            .ThenInclude(cb => cb.FinancialCategory)
            .ThenInclude(fc => fc!.CategoryTransactions!)
            .ThenInclude(ct => ct.Transaction)
            .Where(b => b.AccountBudgets!.Any(ab => ab.Account!.UserId == userId))
            .Select((b) => GetSimpleBudget(b)).ToListAsync();
    }

    private static SimpleBudget GetSimpleBudget(Budget b)
    {
        return new SimpleBudget()
        {
            Id = b.Id,
            Name = b.Name,
            AmountToSave = b.AmountToSave,
            DateTo = b.DateTo,
            DateFrom = b.DateFrom,
            CurrencySymbol = b.Currency!.Symbol,
            SimpleBudgetCategories = b.CategoryBudgets!.Select(ct => new SimpleCategory()
            {
                Id = ct.FinancialCategory!.Id,
                Icon = ct.FinancialCategory.Icon,
                HexColor = ct.FinancialCategory.HexColor,
                Name = ct.FinancialCategory.Name
            }).ToList(),
            AmountSpent = b.CategoryBudgets!
                .SelectMany(cb => cb.FinancialCategory!.CategoryTransactions!)
                .Where(ct =>
                    ct.Transaction!.Time.Date > b.DateFrom.Date && ct.Transaction!.Time.Date < b.DateTo.Date)
                .Sum(cts => cts == null ? 0 : cts.Amount)

        };
    }

    public async Task<BudgetDetails?> GetDetails(Guid userId, Guid budgetId)
    {
        return await RepositoryDbSet
            .Include(b => b.AccountBudgets!)
            .ThenInclude(ab => ab.Account)
            .Include(b => b.Currency)
            .Include(b => b.CategoryBudgets!)
            .ThenInclude(cb => cb.FinancialCategory)
            .ThenInclude(fc => fc!.CategoryTransactions!)
            .ThenInclude(ct => ct.Transaction)
            .Where(b => b.Id == budgetId)
            .Where(b => b.AccountBudgets!.Any(ab => ab.Account!.UserId == userId))
            .Select(b => new BudgetDetails()
            {
                SimpleBudget = GetSimpleBudget(b),
                BudgetCategories = b.CategoryBudgets!.Select(cb => new BudgetCategories()
                {
                    Category = new SimpleCategory()
                    {
                        HexColor = cb.FinancialCategory!.HexColor,
                        Id = cb.FinancialCategory.Id,
                        Icon = cb.FinancialCategory.Icon,
                        Name = cb.FinancialCategory.Name
                    },
                    TotalAmount = cb.FinancialCategory!.CategoryTransactions!
                        .Where(ct => ct.Transaction!.Time.Date > b.DateFrom.Date && ct.Transaction!.Time.Date < b.DateTo.Date)
                        .Sum(ct => ct == null ? 0 : ct.Amount )
                }).ToList(),
                BudgetTransactions = b.CategoryBudgets
                    .SelectMany(cb => cb.FinancialCategory!.CategoryTransactions!.Where(ct => ct.Transaction!.Time.Date > b.DateFrom.Date && ct.Transaction!.Time.Date < b.DateTo.Date))
                    .GroupBy(ct => new {Date = ct.Transaction!.Time.Date})
                    .Select(g => new TransactionDateGroup()
                    {
                        Date = g.Key.Date,
                        CategoryTransactions = g.Select((ct) => new CategoryTransactionDetails()
                        {
                            Amount = ct.Transaction!.CategoryTransactions
                                .Where(ct => b.CategoryBudgets.Any(cb => cb.FinancialCategory!.Id == ct.FinancialCategory!.Id))
                                .Sum((ct) => ct.Amount),
                            TransactionId = ct.Transaction!.Id,
                            Transaction = new TransactionWithCategories()
                            {
                                Transaction = new SimpleTransaction()
                                {
                                    Id = ct.Transaction.Id,
                                    Amount = ct.Transaction.Amount,
                                    SenderReceiver = ct.Transaction.SenderReceiver,
                                    Time = ct.Transaction.Time,
                                    CurrencySymbol = ct.Transaction.Currency!.Symbol
                                },
                                Categories = ct.Transaction.CategoryTransactions!
                                    .Select((e) => new SimpleCategory()
                                    {
                                        HexColor = e.FinancialCategory!.HexColor,
                                        Icon = e.FinancialCategory.Icon,
                                        Name = e.FinancialCategory.Name,
                                        Id = e.FinancialCategory.Id
                                    }).ToList()
                            }
                        }).ToList()
                    }). ToList()
            }).FirstAsync();
    }

    public async Task<BudgetToEdit> GetBudgetToEdit(Guid userId, Guid budgetId)
    {
        return await RepositoryDbSet
            .Where(b => b.Id == budgetId)
            .Where(b => b.AccountBudgets!.Any(ab => ab.Account!.UserId == userId))
            .Select(b => new BudgetToEdit()
            {
                Name = b.Name,
                CurrencyId = b.CurrencyId,
                Currency = new SimpleCurrency()
                {
                    Abbreviation = b.Currency!.Abbreviation,
                    Symbol = b.Currency.Symbol,
                    Id = b.Currency.Id,
                    Name = b.Currency.Name
                },
                Account = b.AccountBudgets!.Select((ac) => new SimpleAccount()
                {
                    Id = ac.Account!.Id,
                    Bank = ac.Account.Bank,
                    Name = ac.Account.Name,
                }).ToList(),
                AmountToSave = b.AmountToSave,
                DateFrom = b.DateFrom,
                DateTo = b.DateTo,
                Id = b.Id,
                SimpleBudgetCategories = b.CategoryBudgets!.Select(ct => new SimpleCategory()
                {
                    Id = ct.FinancialCategory!.Id,
                    Icon = ct.FinancialCategory.Icon,
                    HexColor = ct.FinancialCategory.HexColor,
                    Name = ct.FinancialCategory.Name
                }).ToList(),
            }).AsNoTracking().FirstAsync();
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.AccountBudgets.Where(ab => ab.Account!.UserId == userId).ToList().Count > 0 );
    }

    public override Budget Update(Budget entity)
    {
        var categoriesToRemove =
            RepositoryDbContext.CategoryBudgets
                .Where(cb => cb.BudgetId == entity.Id && !entity.CategoryBudgets!.Contains(cb))
                .ToList();
            
        
        RepositoryDbContext.CategoryBudgets.RemoveRange(categoriesToRemove);
        
        return base.Update(entity);
    }

    public override Task<Budget?> RemoveAsync(Guid id)
    {
        var categoriesToRemove =
            RepositoryDbContext.CategoryBudgets
                .Where(ct => ct.BudgetId == id)
                .ToList();

        var accountsToRemove = RepositoryDbContext
            .AccountBudgets.Where(ab => ab.BudgetId == id).ToList();
            
        
        RepositoryDbContext.CategoryBudgets.RemoveRange(categoriesToRemove);
        RepositoryDbContext.AccountBudgets.RemoveRange(accountsToRemove);
        
        return base.RemoveAsync(id);
    }
    
}