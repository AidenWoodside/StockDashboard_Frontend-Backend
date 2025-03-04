using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public interface IMarketDataProvider
{
    List<Stock> GetStocks(List<string> symbols);
    
    Stock GetStock(string symbol);
}