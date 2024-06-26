namespace Infrastructure.Services;

public interface ICurrencyService
{
    CurrencyRates GetCurrencyRates();
}

public class MockCurrencyService : ICurrencyService
{
    public CurrencyRates GetCurrencyRates()
    {
        return new CurrencyRates
        {
            UsdToTry = 32.95m, // Mock value for USD to TRY
            EurToTry = 35.26m, // Mock value for EUR to TRY
            GoldToTry = 2465.0m // Mock value for Gold (gram) to TRY
        };
    }
}

public class CurrencyRates
{
    public decimal UsdToTry { get; set; }
    public decimal EurToTry { get; set; }
    public decimal GoldToTry { get; set; }
}