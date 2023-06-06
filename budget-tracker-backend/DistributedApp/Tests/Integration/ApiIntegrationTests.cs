

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Public.DTO.v1.Identity;

namespace Tests.Integration;


public class ApiIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public ApiIntegrationTests(CustomWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    private void VerifyJwtContent(string jwt, string email, 
        DateTime validToIsSmallerThan)
    {
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.Jwt);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.Jwt);
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }
    
    private async Task<string> RegisterNewUser(string email, string password, int expiresInSeconds = 1)
    {
        var URL = $"/api/v1/identity/account/register?expiresInSeconds={expiresInSeconds}";

        var registerData = new
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(registerData);
        // Act
        var response = await _client.PostAsync(URL, data);

        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Equal(true, response.IsSuccessStatusCode);

        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;

    }
    
    [Fact(DisplayName = "POST - login user")]
    public async Task LoginUserTest()
    {
        const string email = "login@test.ee";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 1;

        // Arrange
        await RegisterNewUser(email, password, expiresInSeconds);


        var URL = "/api/v1/identity/account/login?expiresInSeconds=1";

        var loginData = new
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginData);

        // Act
        var response = await _client.PostAsync(URL, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(true, response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

    }
    
    [Fact(DisplayName = "CRUD - test all the api CRUD")]
    public async void TestCRUDIntegration()
    {
        var URL = "api/v1/Transactions";
        var EMAIL = "integrationtest@test.ee";
        var PASSWORD = "Foo.Bar1";
        var EXPIRY = 1000;
        //REGISTER THE USER

        var registerRes = await RegisterNewUser(EMAIL, PASSWORD, EXPIRY);
        var jwt = JsonSerializer.Deserialize<JWTResponse>(registerRes, _camelCaseJsonSerializerOptions);
        
        //CRUD - transactions crud part
        
        //CREATE transaction
       
        //ARRANGE
        var currencyId = Guid.Parse("c95992ec-a861-429b-bf61-7258e9c21a1d");
        var categoryId = Guid.Parse("7d9d1cc6-4edf-44b3-938d-7736e46ca94e");
        var handler = new JwtSecurityTokenHandler();
        var claims = handler.ReadJwtToken(jwt!.Jwt).Claims;
        var userId = claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

        //GET cash account
        
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/Accounts");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt);
        
        var response = await _client.SendAsync(request);
        
        var accountId = response.Content.ReadFromJsonAsync<List<Account>>().Result.First().Id;
        
        //BACK to Create transaction
        
        var transactionToPost = new TransactionDetailsDTO
        {
            SenderReceiver = "Test",
            Amount = 10,
            Time = DateTime.Today.ToUniversalTime(),
            CurrencyId = currencyId,
            AccountId = accountId,
            CategoryTransactions = new List<TransactionCategoryDTO>(){ new TransactionCategoryDTO()
            {
                Amount = 10,
                FinancialCategoryId = categoryId
            }}
        };
        
        var postRequest = new HttpRequestMessage(HttpMethod.Post, URL);
        postRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt);
        postRequest.Content = JsonContent.Create(transactionToPost);

        var postResponse = await _client.SendAsync(postRequest);
        Assert.True(postResponse.StatusCode == HttpStatusCode.OK);

        //GET transactions
        
        var getAllRequest =  new HttpRequestMessage(HttpMethod.Get, URL);
        getAllRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt);
        var getAllResponse = await _client.SendAsync(getAllRequest);
        
        var transactions = getAllResponse
            .Content.ReadFromJsonAsync<List<TransactionWithCategories>>().Result;
        
        Assert.NotNull(transactions);
        Assert.Equal(1, transactions.Count);
        Assert.Equal("Test", transactions[0].Transaction!.SenderReceiver);
        Assert.Equal("€", transactions[0].Transaction!.CurrencySymbol);
        Assert.Equal(1, transactions[0].Categories!.Count);
        Assert.Equal("Shopping", transactions[0].Categories!.First().Name);
        
        //UPDATE transaction
        var transactionToPut = new TransactionDetailsDTO
        {
            Id = transactions[0].Transaction!.Id,
            SenderReceiver = "Test put",
            Amount = 15,
            Time = DateTime.Today.ToUniversalTime(),
            CurrencyId = currencyId,
            AccountId = accountId,
            CategoryTransactions = new List<TransactionCategoryDTO>(){ new TransactionCategoryDTO()
            {
                Amount = 10,
                FinancialCategoryId = categoryId
            }}
        };
        var putRequest = new HttpRequestMessage(HttpMethod.Put, URL + "/" + transactions[0].Transaction!.Id);
        putRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt);
        putRequest.Content = JsonContent.Create(transactionToPut);
        
        var putResponse = await _client.SendAsync(putRequest);
        Assert.True(putResponse.StatusCode == HttpStatusCode.OK);
        
        var transactionPut = putResponse
            .Content.ReadFromJsonAsync<TransactionDetailsDTO>().Result;
        
        Assert.NotNull(transactionPut);
        Assert.Equal("Test put", transactionPut!.SenderReceiver);
        Assert.Equal(15, transactionPut.Amount);
        
        //DELETE transaction
        var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, URL + "/" + transactions[0].Transaction!.Id);
        deleteRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt);
       
        var deleteResponse = await _client.SendAsync(deleteRequest);
        
        
        Assert.True(deleteResponse.StatusCode == HttpStatusCode.NoContent);

        //Check if actually deleted
        var getAllRequest2 =  new HttpRequestMessage(HttpMethod.Get, URL);
        getAllRequest2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt);
        var getAllResponse2 = await _client.SendAsync(getAllRequest2);
        
        var transactionsEmpty = getAllResponse2
            .Content.ReadFromJsonAsync<List<TransactionWithCategories>>().Result;
        
        Assert.Empty(transactionsEmpty);
        
        //LOGOUT user 
        var logoutRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/identity/account/logout");
        logoutRequest.Content = JsonContent.Create(new Logout(){RefreshToken = jwt.RefreshToken});
        var logoutResponse = await _client.SendAsync(logoutRequest);
        Assert.True(logoutResponse.StatusCode == HttpStatusCode.Found);
        
    }
}