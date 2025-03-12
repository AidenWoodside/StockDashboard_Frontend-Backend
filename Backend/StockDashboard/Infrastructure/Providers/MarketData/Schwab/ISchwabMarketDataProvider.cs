using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab;

public interface ISchwabMarketDataProvider
{
    
    Task<SchwabStockQuoteResponse> GetStockBySymbol(string symbol);
}