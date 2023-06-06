using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.APP;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<AccountBudget> AccountBudgets { get; set; } = default!;
    public DbSet<Budget> Budgets { get; set; } = default!;
    public DbSet<CategoryBudget> CategoryBudgets { get; set; } = default!;
    public DbSet<CategoryTransaction> CategoryTransactions { get; set; } = default!;
    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<FinancialCategory> FinancialCategories { get; set; } = default!;
    public DbSet<Subscription> Subscriptions { get; set; } = default!;
    public DbSet<SubscriptionType> SubscriptionTypes { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    
    public DbSet<ApplicationRefreshToken> ApplicationRefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // disable cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        // other configuration options
    }

}