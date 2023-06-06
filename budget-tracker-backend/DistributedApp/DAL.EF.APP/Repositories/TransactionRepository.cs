using System.Runtime.InteropServices.ComTypes;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.BASE;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DAL.EF.APP.Repositories;

public class TransactionRepository: EFBaseRepository<Transaction, AppDbContext>, ITransactionRepository
{
    public TransactionRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<Transaction>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.CategoryTransactions!)
            .ThenInclude(ct => ct.FinancialCategory)
            .Where(t => t.Account!.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TransactionWithCategories?>> GetTransactionsWithCategories(Guid userId)
    {
        return await RepositoryDbSet.Where(t => t.Account!.UserId == userId)
            .Select((e) => new TransactionWithCategories()
            {
                Id = e.Id,
                Transaction = new SimpleTransaction()
                {
                    Amount = e.Amount,
                    CurrencySymbol = e.Currency!.Symbol,
                    Id = e.Id,
                    SenderReceiver = e.SenderReceiver,
                    Time = e.Time
                },
                Categories = e.CategoryTransactions!.Select((ct) => new SimpleCategory
                {
                    Id = ct.FinancialCategory!.Id,
                    Name = ct.FinancialCategory.Name,
                    HexColor = ct.FinancialCategory.HexColor,
                    Icon = ct.FinancialCategory.Icon
                }).ToList()
            }).ToListAsync();
    }

    public async Task<TransactionDetails?> GetDetails(Guid userId, Guid transactionId)
    {
        return await RepositoryDbSet
            .Include(e => e.CategoryTransactions!)
            .ThenInclude(ct => ct.FinancialCategory)
            .Where(t => t.Account!.UserId == userId)
            .Where(t => t.Id == transactionId)
            .Select(t => new TransactionDetails()
            {
                Id = t.Id,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Currency = new SimpleCurrency()
                {
                    Id = t.Currency!.Id,
                    Abbreviation = t.Currency.Abbreviation,
                    Name = t.Currency.Name,
                    Symbol = t.Currency.Symbol
                },
                CurrencyId = t.Currency.Id,
                SenderReceiver = t.SenderReceiver,
                Time = t.Time,
                CategoryTransactions = t.CategoryTransactions!.Select((ct) => new TransactionCategory()
                {
                    Id = ct.Id,
                    Amount = Math.Round(ct.Amount, 2),
                    FinancialCategoryId = ct.FinancialCategory!.Id,
                    FinancialCategory = new SimpleCategory()
                    {
                        Id = ct.FinancialCategory.Id,
                        HexColor = ct.FinancialCategory.HexColor,
                        Icon = ct.FinancialCategory.Icon,
                        Name = ct.FinancialCategory.Name
                    }
                }).ToList()
            }).FirstAsync()
            ;
    }

    public async Task<IEnumerable<TransactionGraphData>> GetGraphData(Guid userId)
    {
        DateTime today = DateTime.Today;
        int daysSinceMonday = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        DateTime monday = today.AddDays(-daysSinceMonday);


        return await RepositoryDbSet
            .Where(t => t.Account!.UserId == userId && t.Time.ToUniversalTime() >= monday.ToUniversalTime())
            .GroupBy(t => new {Date = t.Time})
            .Select(g => new TransactionGraphData(){ 
                WeekDay = new DateTime(g.Key.Date.Year, g.Key.Date.Month, g.Key.Date.Day).ToString("ddd"), 
                TotalAmount = g.Sum(t => t.Amount) })
            .ToListAsync();
    }
 
    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.Account!.UserId == userId );
    }

    public override Task<Transaction?> RemoveAsync(Guid id)
    {
        var categoriesToRemove =
            RepositoryDbContext.CategoryTransactions
                .Where(ct => ct.TransactionId == id)
                .ToList();
            
        
        RepositoryDbContext.CategoryTransactions.RemoveRange(categoriesToRemove);
        return base.RemoveAsync(id);
    }
    

    public override Transaction Update(Transaction entity)
    {
        var categoriesToRemove =
            RepositoryDbContext.CategoryTransactions
                .Where(ct => ct.TransactionId == entity.Id && !entity.CategoryTransactions!.Contains(ct))
                .ToList();
            
        
        RepositoryDbContext.CategoryTransactions.RemoveRange(categoriesToRemove);
        
        return base.Update(entity);
    }
}