namespace StockDashboard.Infrastructure.Providers.Trading.Schwab;

public interface ISchwabTokenProvider
{
    public Task<string> GetAccessToken();
}