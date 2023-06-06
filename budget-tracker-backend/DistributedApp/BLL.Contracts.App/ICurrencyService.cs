namespace BLL.Contracts.App;

public interface ICurrencyService
{
    Task<IEnumerable<BLL.DTO.Currency>> AllSimpleCurrencyAsync();
}