using System.Diagnostics;
using AutoMapper;
using BLL.App;
using BLL.App.Mappers;
using BLL.App.Serivices;
using DAL.EF.APP;
using DAL.EF.APP.Seeding;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Core.Types;
using Public.DTO;
using Public.DTO.v1;
using Tests.Services;
using WebApp.ApiControllers;
using GetUserIdService = Tests.Services.GetUserIdService;

namespace Tests;

public class ApiTransactionsInitTest
{
    private readonly AppDbContext context;
    private readonly TransactionsController _controller;
    private static Guid _adminId1 = Guid.Parse("65774695-bd99-4096-a0fb-7494da17b322");
    private static Guid _adminId2 = Guid.Parse("65884695-bd99-4096-a0fb-7494da17b322");
    private static Guid _currencyId;
    private static Guid _accountId;
    private static Guid _categoryId;
    private static Guid _transactionId1;
    private static Guid _transactionId2;

    public ApiTransactionsInitTest()
    {
        // set up mock database - inMemory

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        context = new AppDbContext(optionsBuilder.Options);

        // reset db
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutomapperConfig());
            cfg.AddProfile(new AutoMapperConfig());
        });

        var mapper = mockMapper.CreateMapper();

        var Uow = new AppUOW(context);
        
        var bll = new AppBLL(Uow, mapper); 
        
        _controller = new TransactionsController(mapper, bll, new GetUserIdService());
    }
    
    [Fact(DisplayName = "GET - all transactions")]
    public async void TestGetAllTransactions()
    {
    
    //test case is failed when an exception is thrown
    await SeedDataAsync();

    // arrange - SUT
    
    //act
    var res =  _controller.GetTransactions().Result;
    //assert
    Assert.NotNull(res);
    var transactionDtos = res.ToList();
    Assert.Single(transactionDtos);
    
    var transactionDTO = transactionDtos.First();

    Assert.Equal(14.56, transactionDTO!.Transaction!.Amount);
    Assert.Equal(1, transactionDTO.Categories!.Count());
    Assert.NotNull(transactionDTO.Categories!.First());
    Assert.Equal("Shopping", transactionDTO.Categories!.First().Name);
    Assert.Equal("€",transactionDTO.Transaction.CurrencySymbol);
    }
    
    [Fact(DisplayName = "Post - post transaction")]
    public async void TestPostTransaction()
    {
    
        //test case is failed when an exception is thrown
        await SeedDataAsync();

        // arrange - SUT
        var transactionToPost = new TransactionDetailsDTO
        {
            SenderReceiver = "Test",
            Amount = 10,
            Time = DateTime.Today.ToUniversalTime(),
            CurrencyId = _currencyId,
            AccountId = _accountId,
            CategoryTransactions = new List<TransactionCategoryDTO>(){ new TransactionCategoryDTO()
            {
                Amount = 10,
                FinancialCategoryId = _categoryId
            }}
        };
        //act
        var res = _controller.PostTransaction(transactionToPost).Result.Value;
        //assert
        Assert.NotNull(res);
        Assert.Equal(10, res.Amount);
        Assert.Equal("Test", res.SenderReceiver);
        Assert.NotNull(res.CategoryTransactions);
        Assert.Equal(1, res.CategoryTransactions.Count);
        Assert.Equal(10, res.CategoryTransactions.First().Amount);
        Assert.Equal(_categoryId, res.CategoryTransactions.First().FinancialCategoryId);
    }
    
    [Fact(DisplayName = "Put - put transaction")]
    public async void TestPutTransaction()
    {
    
        //test case is failed when an exception is thrown
        await SeedDataAsync();

        // arrange - SUT
        var transactionToPut = new TransactionDetailsDTO
        {
            Id = _transactionId1,
            SenderReceiver = "Test put",
            Amount = 10,
            Time = DateTime.Today.ToUniversalTime(),
            CurrencyId = _currencyId,
            AccountId = _accountId,
            CategoryTransactions = new List<TransactionCategoryDTO>(){ new TransactionCategoryDTO()
            {
                Amount = 10,
                FinancialCategoryId = _categoryId
            }}
        };
        //act
        var res = _controller.PutTransaction(_transactionId1,transactionToPut).Result.Value;
        //assert
        Assert.NotNull(res);
        Assert.Equal(_transactionId1, res.Id);
        Assert.Equal(10, res.Amount);
        Assert.Equal("Test put", res.SenderReceiver);
        Assert.NotNull(res.CategoryTransactions);
        Assert.Equal(1, res.CategoryTransactions.Count);
        Assert.Equal(10, res.CategoryTransactions.First().Amount);
        Assert.NotNull(res.CategoryTransactions.First().FinancialCategory);
        Assert.Equal(_categoryId, res.CategoryTransactions.First().FinancialCategory!.Id);
    }
    
    [Fact(DisplayName = "PUT - wrong user")]
    public async Task TestPutTransactionWithWrongUser()
    {
        //arrange
        await SeedDataAsync();

        // Act
        
        var transactionToPut = new TransactionDetailsDTO
        {
            Id = _transactionId2,
            SenderReceiver = "Test put worng user",
            Amount = 10,
            Time = DateTime.Today.ToUniversalTime(),
            CurrencyId = _currencyId,
            AccountId = _accountId,
            CategoryTransactions = new List<TransactionCategoryDTO>(){ new TransactionCategoryDTO()
            {
                Amount = 10,
                FinancialCategoryId = _categoryId
            }}
        };
        
        var result = _controller.PutTransaction(_transactionId2, transactionToPut).Result;
        Assert.IsType(typeof(BadRequestObjectResult), result.Result);
    }

    [Fact(DisplayName = "DELETE - delete transaction")]
    public async Task TestDeleteTransaction()
    {
        //arrange
        await SeedDataAsync();

        // Act
        var delete =  _controller.DeleteTransaction(_transactionId1).Result;

        var result = _controller.GetTransactions().Result;
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact(DisplayName = "Get - get transaction details")]
    public async void TestGetTransactionDetails()
    {
    
        //test case is failed when an exception is thrown
        await SeedDataAsync();

        // arrange - SUT
        //act
        var res = _controller.GetTransactionDetails(_transactionId1).Result.Value;
        //assert
        Assert.NotNull(res);
        Assert.Equal(_transactionId1, res.Id);
        Assert.Equal(14.56, res.Amount);
        Assert.Equal("Aleksander Kampus", res.SenderReceiver);
        Assert.NotNull(res.CategoryTransactions);
        Assert.Equal(1, res.CategoryTransactions.Count);
        Assert.Equal(12.99, res.CategoryTransactions.First().Amount);
        Assert.NotNull(res.CategoryTransactions.First().FinancialCategory);
        Assert.Equal(_categoryId, res.CategoryTransactions.First().FinancialCategory!.Id);
    }
    
    [Fact(DisplayName = "Get - get transaction graph data")]
    public async void TestGetTransactionGraphData()
    {
    
        //test case is failed when an exception is thrown
        await SeedDataAsync();

        // arrange - SUT
        //act
        var res = _controller.GetTransactionGraphData().Result.Value;
        //assert
        Assert.NotNull(res);
        Assert.Single(res);
        Assert.Equal(14.56, res.First().TotalAmount);
        Assert.Equal("Thu", res.First().WeekDay);
    }
    private async Task SeedDataAsync()
    {
        var category =context.FinancialCategories.Add(new FinancialCategory()
            {
                Name = "Shopping",
                HexColor = "#3A49F9",
                Icon = AppDataInit.SvgFileToByteArray("../../../../DAL.EF.APP/Icons/ShoppingIcon.svg")
            }
        );
        _categoryId = category.Entity.Id;
        var euro = context.Currencies.Add(new Currency
        {
            Name = "Euro",
            Abbreviation = "EUR",
            Symbol = "€",
        });
        var currencyId = euro.Entity.Id;
        _currencyId = euro.Entity.Id;
        
        (Guid id, string email, string pwd) userData = (_adminId1, "admin@budget.com", "Budget_Tracker1");
        context.Users.Add(new ApplicationUser()
        {
            Id = userData.id,
            Email = userData.email,
            UserName = userData.email,
            EmailConfirmed = true,
        });

        var account = context.Accounts.Add(new Account()
        {
            Name = "Cash Account",
            Bank = "Cash",
            UserId = _adminId1,
        });
        var account2 = context.Accounts.Add(new Account()
        {
            Name = "Cash Account",
            Bank = "Cash",
            UserId = _adminId2,
        });
        _accountId = account.Entity.Id;
        var transaction = context.Transactions.Add(new Transaction
        {
            SenderReceiver = "Aleksander Kampus",
            Amount = 14.56,
            CurrencyId = currencyId,
            AccountId = account.Entity.Id,
        });
        context.CategoryTransactions.Add(new CategoryTransaction
        {
            TransactionId = transaction.Entity.Id,
            CategoryId = category.Entity.Id,
            Amount = 12.99,
        });
        var transaction2 = context.Transactions.Add(new Transaction
        {
            SenderReceiver = "Aleksander Kampus 2",
            Amount = 15.56,
            CurrencyId = currencyId,
            AccountId = account2.Entity.Id,
        });
        context.CategoryTransactions.Add(new CategoryTransaction
        {
            TransactionId = transaction.Entity.Id,
            CategoryId = category.Entity.Id,
            Amount = 12.99,
        });
        _transactionId1 = transaction.Entity.Id;
        _transactionId2 = transaction2.Entity.Id;
        await context.SaveChangesAsync();
    }

}