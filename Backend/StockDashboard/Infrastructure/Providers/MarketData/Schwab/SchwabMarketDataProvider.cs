using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab;

public class SchwabMarketDataProvider : ISchwabMarketDataProvider
{
    public List<Stock> GetStocks(List<string> symbols)
    {
        throw new NotImplementedException();
    }

    public Stock GetStock(string symbol)
    {
        throw new NotImplementedException();
    }
}