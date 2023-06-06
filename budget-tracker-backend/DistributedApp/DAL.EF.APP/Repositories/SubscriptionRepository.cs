using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.BASE;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.APP.Repositories;

public class SubscriptionRepository: EFBaseRepository<Subscription, AppDbContext>, ISubscriptionRepository
{
    public SubscriptionRepository(AppDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.Account!.UserId == userId );
    }

    public async Task<IEnumerable<SimpleSubscription>> AllSimpleAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Where(s => s.Account!.UserId == userId)
            .Select(s => new SimpleSubscription()
            {
                Id = s.Id,
                Amount = s.Amount,
                DateStarted = s.Date,
                Name = s.Name,
                SubscriptionType = new DAL.DTO.SubscriptionType()
                {
                    Name = s.SubscriptionType!.Name
                },
                SubscriptionTypeId = s.SubscriptionTypeId,
                NextPayment = GetNextPayment(s.Date, s.SubscriptionType.Name)
            }).ToListAsync();
    }

    public async Task<SubscriptionDetails>  GetSubscriptionDetails(Guid userId, Guid id)
    {
        return await RepositoryDbSet
            .Where(s => s.Account!.UserId == userId && s.Id == id)
            .Select(s => new SubscriptionDetails
            {
                Id = s.Id,
                Name = s.Name,
                Amount = s.Amount,
                DateStarted = s.Date,
                SubscriptionTypeId = s.SubscriptionTypeId,
                AccountId = s.AccountId,
                CurrencyId = s.CurrencyId,
                Currency = new SimpleCurrency()
                {
                    Id = s.Currency!.Id,
                    Abbreviation = s.Currency.Abbreviation,
                    Name = s.Currency.Name,
                    Symbol = s.Currency.Symbol
                },
            }).FirstAsync();
    }

    private static DateTime GetNextPayment(DateTime dateStarted, string subsType)
    {
        DateTime nextPaymentDate;

        if (subsType == "Monthly")
        {
            // Add one month to the start date to get the next payment date
            nextPaymentDate = dateStarted.AddMonths(1);
        }
        else if (subsType == "Yearly")
        {
            // Add one year to the start date to get the next payment date
            nextPaymentDate = dateStarted.AddYears(1);
        }
        else
        {
            throw new ArgumentException("Invalid subscription type.");
        }

        // If the next payment date is in the past, add another subscription period to get the next payment date
        while (nextPaymentDate < DateTime.Today)
        {
            if (subsType == "Monthly")
            {
                nextPaymentDate = nextPaymentDate.AddMonths(1);
            }
            else if (subsType == "Yearly")
            {
                nextPaymentDate = nextPaymentDate.AddYears(1);
            }
        }

        return nextPaymentDate;
    }
    
}