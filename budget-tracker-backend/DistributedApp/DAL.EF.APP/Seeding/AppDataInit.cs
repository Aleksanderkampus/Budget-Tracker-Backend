using System.Text.Json;
using System.Xml;
using currencyapi;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DAL.EF.APP.Seeding;

public static class AppDataInit
{
    private static Guid _adminId = Guid.Parse("65774695-bd99-4096-a0fb-7494da17b322");
    private static Guid _categoryID = Guid.Parse("7d9d1cc6-4edf-44b3-938d-7736e46ca94e");
    private static Guid _accountId;
    private static Guid _currencyId;
    private static Guid _subsType; 
    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        (Guid id, string email, string pwd) userData = (_adminId, "admin@budget.com", "Budget_Tracker1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user == null)
        {
            user = new ApplicationUser()
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                EmailConfirmed = true,
            };
            var result = userManager.CreateAsync(user, userData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result.ToString()}");
            }
           
        }
    }

    public static void SeedAppData(AppDbContext context)
    {
        SeedAppDataAccounts(context);
        SeedAppDataAccount(context);
        SeedAppDataCurrency(context);
        SeedAppDataTransaction(context);
        SeedAppDataBudgets(context);
        SeedAppDataSubscriptionTypes(context);
        SeedAppDataSubscriptions(context);
        context.SaveChanges();
    }

    private static void SeedAppDataAccount(AppDbContext context)
    {
        if (context.FinancialCategories.Any()) return;

        var shopping = context.FinancialCategories.Add(new FinancialCategory()
            {
               Name = "Shopping",
               HexColor = "#3A49F9",
               Icon = SvgFileToByteArray("../DAL.EF.APP/Icons/ShoppingIcon.svg")
            }
        );
        _categoryID = shopping.Entity.Id;
        
        context.FinancialCategories.Add(new FinancialCategory()
            {
                Name = "Housing",
                HexColor = "#ED713C",
                Icon = SvgFileToByteArray("../DAL.EF.APP/Icons/HousingIcon.svg")
            }
        );
    }
    
    private static void SeedAppDataAccounts(AppDbContext context)
    {
        if (context.Accounts.Any()) return;
        var account = context.Accounts.Add(new Account()
        {
            UserId = _adminId,
            Bank = "Cash Account",
            Name = "Cash Account",
        });
        _accountId = account.Entity.Id;
    }

    private static void SeedAppDataCurrency(AppDbContext context)
    {
        if (context.Currencies.Any()) return;
        var fx = new Currencyapi("8vQX0ql3ybIZDPCMrTj1PxXdSnTcsRbSHgGuzNrW");

        var currencies = JsonDocument.Parse(fx.Currencies()).RootElement;

        var data = currencies.GetProperty("data");

        foreach (var currency in data.EnumerateObject())
        {
            context.Currencies.Add(new Currency
            {
                Name = currency.Value.GetProperty("name").GetString()!,
                Abbreviation = currency.Value.GetProperty("code").GetString()!,
                Symbol = currency.Value.GetProperty("symbol_native").GetString()!,
            });
            
        }
        var euro = context.Currencies.Add(new Currency
        {
            Name = "Euro",
            Abbreviation = "EUR",
            Symbol = "€",
        });
        _currencyId = euro.Entity.Id;
        context.Currencies.Add(new Currency
        {
            Name = "USA Dollar",
            Abbreviation = "USD",
            Symbol = "$",
        });
    }
    private static void SeedAppDataTransaction(AppDbContext context)
    {
        if (context.Transactions.Any()) return;

        var transaction = context.Transactions.Add(new Transaction
        {
            SenderReceiver = "Aleksander Kampus",
            Amount = 14.56,
            CurrencyId = _currencyId,
            AccountId = _accountId,
        });
        context.CategoryTransactions.Add(new CategoryTransaction
        {
            TransactionId = transaction.Entity.Id,
            CategoryId = _categoryID,
        });
    }

    private static void SeedAppDataSubscriptionTypes(AppDbContext context)
    {
        if (context.SubscriptionTypes.Any()) return;

        var monthly = context.SubscriptionTypes.Add(new SubscriptionType()
        {
            Name = "Monthly"
        });
        _subsType = monthly.Entity.Id;
        context.SubscriptionTypes.Add(new SubscriptionType()
        {
            Name = "Yearly"
        });
        
    }
    
    private static void SeedAppDataSubscriptions(AppDbContext context)
    {
        if (context.Subscriptions.Any()) return;

        context.Subscriptions.Add(new Subscription
        {
            Name = "Test",
            Amount = 9.99,
            Date = DateTime.Today.ToUniversalTime(),
            AccountId = _accountId,
            CurrencyId = _currencyId,
            SubscriptionTypeId = _subsType,
        });

    }
    private static void SeedAppDataBudgets(AppDbContext context)
    {
        if (context.Budgets.Any()) return;

        var budget = context.Budgets.Add(new Budget()
        {
            Name = "Test budget",
            AmountToSave = 200,
            DateFrom = DateTime.Today.ToUniversalTime(),
            DateTo = DateTime.Today.AddDays(5).ToUniversalTime(),
            CurrencyId = _currencyId
        });
        context.CategoryBudgets.Add(new CategoryBudget()
        {
            BudgetId = budget.Entity.Id,
            CategoryId = _categoryID
        });
        context.AccountBudgets.Add(new AccountBudget()
        {
            AccountId = _accountId,
            BudgetId = budget.Entity.Id
        });
    }
    public static byte[] SvgFileToByteArray(string filePath)
    {
        string svgXml = File.ReadAllText(filePath);
        return SvgToByteArray(svgXml);
    }
    private static byte[] SvgToByteArray(string svgXml)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create(ms, settings))
            {
                writer.WriteRaw(svgXml);
                writer.Flush();
                return ms.ToArray();
            }
        }
    }
}

