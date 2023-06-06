using AutoMapper;
using DAL.DTO;
using Public.DTO.v1;
using PieChartData = Public.DTO.v1.PieChartData;


namespace Public.DTO;

public class AutomapperConfig: Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Currency, CurrencyDTO>().ReverseMap(); 
        CreateMap<BLL.DTO.SimpleAccount, Public.DTO.v1.SimpleAccount>().ReverseMap();

        CreateMap<BLL.DTO.Categories.SimpleCategory, SimpleCategoryDTO>().ReverseMap();
        CreateMap<BLL.DTO.Categories.CategoryWithTransactionAndBudget, CategoryWithTransactionAndBudgetDTO>();
        CreateMap<BLL.DTO.Categories.CategoryDetails, CategoryDetailsDTO>();
        CreateMap<BLL.DTO.Categories.CategoryTransactionDetails, CategoryTransactionDetailsDTO>();
        CreateMap<BLL.DTO.Categories.GraphData, GraphDataDTO>();
        CreateMap<BLL.DTO.Categories.TransactionDateGroup, TransactionDateGroupDTO>();
        CreateMap<BLL.DTO.Categories.PieChartData, PieChartData>();
        
        CreateMap<BLL.DTO.Transactions.SimpleTransaction, SimpleTransactionDTO>();
        CreateMap<BLL.DTO.Transactions.TransactionCategory, TransactionCategoryDTO>().ReverseMap();
        CreateMap<BLL.DTO.Transactions.TransactionDetails, TransactionDetailsDTO>().ReverseMap();
        CreateMap<BLL.DTO.Transactions.TransactionWithCategories, Public.DTO.v1.TransactionWithCategories>();
        CreateMap<BLL.DTO.Transactions.TransactionGraphData, Public.DTO.v1.TransactionGraphData>();
        
        CreateMap<BLL.DTO.Budgets.SimpleBudget, SimpleBudgetDTO>();
        CreateMap<BLL.DTO.Budgets.BudgetDetails, BudgetDetailsDTO>();
        CreateMap<BLL.DTO.Budgets.BudgetCategories, BudgetCategoriesDTO>();
        CreateMap<BLL.DTO.Budgets.BudgetToEdit, Public.DTO.v1.BudgetToEdit>().ReverseMap();

        CreateMap<BLL.DTO.SimpleSubscription, Public.DTO.v1.SimpleSubscription>();
        CreateMap<BLL.DTO.SubscriptionType, Public.DTO.v1.SubscriptionType>().ReverseMap();
        CreateMap<BLL.DTO.SubscriptionDetails, Public.DTO.v1.SubscriptionDetails>().ReverseMap();
    }

    
}