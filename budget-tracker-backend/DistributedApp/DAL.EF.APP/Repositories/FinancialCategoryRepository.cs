using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.BASE;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.APP.Repositories;

public class FinancialCategoryRepository: EFBaseRepository<FinancialCategory, AppDbContext>, IFinancialCategoryRepository
{
    public FinancialCategoryRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<FinancialCategory>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.CategoryTransactions!)
                .ThenInclude(e => e.Transaction)
            .OrderBy(e => e.Name)
            .ToListAsync();
    }
    
    public override async Task<IEnumerable<FinancialCategory>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            //.Include(e => e.AppUser)
            .Include(e => e.CategoryTransactions!.Where(ct => ct.Transaction!.Account!.UserId == userId))
            .ThenInclude(e => e.Transaction)
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<SimpleCategory>> AllSimpleCategoriesAsync()
    {
        return await RepositoryDbSet.Select((c) => new SimpleCategory()
        {
            Id = c.Id,
            HexColor = c.HexColor,
            Name = c.Name,
            Icon = c.Icon,
        }).ToListAsync();
    }

    public async Task<IEnumerable<CategoryWithTransactionAndBudget>> AllCategoryWithTransactionAndBudgetAsync(Guid userId)
    {
        return await RepositoryDbSet
            //.Include(e => e.AppUser)
            .Include(e => e.CategoryTransactions!.Where(ct => ct.Transaction!.Account!.UserId == userId))
            .ThenInclude(e => e.Transaction)
            .OrderBy(e => e.Name)
            .Select((c) => new CategoryWithTransactionAndBudget()
            {
                Id = c.Id,
                HexColor = c.HexColor,
                Name = c.Name,
                Icon = c.Icon,
                NumberOfTransactions = c.CategoryTransactions!.Count,
                NumberOfBudgets = c.CategoryBudgets!.Count,
                AmountSpent = c.CategoryTransactions.Sum((ct) => ct.Amount)
            })
            .ToListAsync();
    }
    public async Task<CategoryDetails> GetCategoryDetails(Guid userId, Guid id)
    {
        
        return await RepositoryDbSet
            //.Include(e => e.AppUser)
            .Where(cd => cd.Id == id)
            .Include(e => e.CategoryTransactions!.Where(ct => ct.Transaction!.Account!.UserId == userId))
            .ThenInclude(e => e.Transaction)
            .OrderBy(e => e.Name)
            .Select((c) => new CategoryDetails()
            {
                Id = c.Id,
                HexColor = c.HexColor,
                Icon = c.Icon!,
                Name = c.Name,
                AmountSpent = c.CategoryTransactions.Sum((ct) => ct.Amount),
                GraphData = c.CategoryTransactions
                    .Where(ct => ct.Transaction!.Time >= DateTime.Today.AddMonths(-6))
                    .GroupBy(ct => new { Month = ct.Transaction!.Time.Month, Year= ct.Transaction!.Time.Year })
                    .Select(g => new GraphData()
                    {
                        Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                        Amount = g.Sum(ct => ct.Amount)
                    })
                    .ToList(),
                CategoryTransactionsDateGroups = c.CategoryTransactions
                    .GroupBy(ct => new {Date = ct.Transaction!.Time.Date})
                    .Select(g => new TransactionDateGroup()
                    {
                        Date = g.Key.Date,
                        CategoryTransactions = g.Select((ct) => new CategoryTransactionDetails()
                        {
                            Amount = ct.Amount,
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
            })
            .FirstAsync();
    }

    public async Task<IEnumerable<PieChartData>> GetPieChartData(Guid userId)
    {
        return await RepositoryDbSet
            .Select(c => new PieChartData()
            {
                HexColor = c.HexColor,
                Name = c.Name,
                TotalAmount = c.CategoryTransactions!
                    .Where(ct => ct.Transaction!.Account!.UserId == userId)
                    .Sum(e => e.Amount)
            }).ToListAsync();
    }
}