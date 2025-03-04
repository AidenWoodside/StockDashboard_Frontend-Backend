using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData.Alpaca;

public class AlpacaMarketDataProvider : IAlpacaMarketDataProvider
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