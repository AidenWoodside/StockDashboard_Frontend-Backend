namespace StockDashboard.Infrastructure.Providers.Trading.Schwab;

public interface ISchwabTradingProvider
{
    Task<T> GetAccounts<T>();
    
    Task<T> GetEncryptedAccounts<T>();
}