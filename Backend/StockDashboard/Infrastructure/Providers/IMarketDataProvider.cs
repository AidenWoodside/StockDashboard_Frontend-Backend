using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers;

public interface IMarketDataProvider
{
    public Task<Stock> GetStocksAsync(string symbol);
}