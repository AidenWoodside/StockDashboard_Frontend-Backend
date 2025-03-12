using Microsoft.AspNetCore.Mvc;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab.Models;

namespace StockDashboard.API.Services;

public class MarketDataService(ISchwabMarketDataProvider marketDataProvider) : IMarketDataService
{
    public async Task<SchwabStockQuoteResponse> GetStockBySymbol(string symbol)
    {
        return await marketDataProvider.GetStockBySymbol(symbol);
    }
}