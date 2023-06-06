using AutoMapper;
using Domain;

namespace BLL.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DAL.DTO.SimpleCategory, BLL.DTO.Categories.SimpleCategory>();
        CreateMap<DAL.DTO.CategoryDetails, BLL.DTO.Categories.CategoryDetails>();
        CreateMap<DAL.DTO.CategoryTransactionDetails, BLL.DTO.Categories.CategoryTransactionDetails>();
        CreateMap<DAL.DTO.SimpleTransaction, BLL.DTO.Transactions.SimpleTransaction>();
        CreateMap<DAL.DTO.GraphData, BLL.DTO.Categories.GraphData>();
        CreateMap<DAL.DTO.TransactionDateGroup, BLL.DTO.Categories.TransactionDateGroup>();
        CreateMap<DAL.DTO.CategoryWithTransactionAndBudget, BLL.DTO.Categories.CategoryWithTransactionAndBudget>();
        CreateMap<DAL.DTO.PieChartData, BLL.DTO.Categories.PieChartData>();
        
        CreateMap<DAL.DTO.TransactionCategory, BLL.DTO.Transactions.TransactionCategory>();
        CreateMap<DAL.DTO.TransactionDetails, BLL.DTO.Transactions.TransactionDetails>();
        CreateMap<DAL.DTO.TransactionWithCategories, BLL.DTO.Transactions.TransactionWithCategories>();
        CreateMap<DAL.DTO.TransactionGraphData, BLL.DTO.Transactions.TransactionGraphData>();
        
        CreateMap<BLL.DTO.Transactions.TransactionDetails, Domain.Transaction>()
            .ForMember(dest => dest.CategoryTransactions
                , options 
                    => options.MapFrom(src 
                        => src.CategoryTransactions!.Select(e => new CategoryTransaction()
                        {
                            Id = e.Id,
                            CategoryId = e.FinancialCategoryId,
                            Amount = e.Amount,
                        }).ToList())).ReverseMap();
        
        CreateMap<Domain.CategoryTransaction, BLL.DTO.Transactions.TransactionCategory>().ReverseMap();
        
        CreateMap<DAL.DTO.SimpleCurrency, BLL.DTO.Currency>();

        CreateMap<DAL.DTO.SimpleAccount, BLL.DTO.SimpleAccount>();

        CreateMap<BLL.DTO.Budgets.BudgetToEdit, Domain.Budget>()
            .ForMember(dest => dest.CategoryBudgets
                ,options 
                    => options.MapFrom(src => src.SimpleBudgetCategories
                        .Select(e => new CategoryBudget()
                        {
                            CategoryId = e.Id
                        })) )
            .ForMember(dest => dest.AccountBudgets, 
                options => 
                    options.MapFrom(src => src.Account!.Select(e => new AccountBudget()
                    {
                        AccountId = e.Id
                    }).ToList()))
            .ReverseMap();
            

        CreateMap<BLL.DTO.Currency, Domain.Currency>().ReverseMap();
        CreateMap<BLL.DTO.SimpleAccount, Domain.Account>().ReverseMap();
        CreateMap<BLL.DTO.Categories.SimpleCategory, Domain.FinancialCategory>().ReverseMap();

        
        CreateMap<DAL.DTO.SimpleBudget,BLL.DTO.Budgets.SimpleBudget>();
        CreateMap<DAL.DTO.BudgetDetails,BLL.DTO.Budgets.BudgetDetails>();
        CreateMap<DAL.DTO.BudgetCategories,BLL.DTO.Budgets.BudgetCategories>();
        CreateMap<DAL.DTO.BudgetToEdit, BLL.DTO.Budgets.BudgetToEdit>();
        
        CreateMap<Domain.SubscriptionType, BLL.DTO.SubscriptionType>();
        CreateMap<Domain.Subscription, BLL.DTO.SubscriptionDetails>().ReverseMap()
            .ForMember(dest => dest.Date
                ,options 
                    => options.MapFrom(src => src.DateStarted) );
        
        CreateMap<DAL.DTO.SimpleSubscription, BLL.DTO.SimpleSubscription>();
        CreateMap<DAL.DTO.SubscriptionType, BLL.DTO.SubscriptionType>();
        CreateMap<DAL.DTO.SubscriptionDetails, BLL.DTO.SubscriptionDetails>();

    }
}