using Microsoft.AspNetCore.Mvc;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab.Models;

namespace StockDashboard.API.Services;

public interface IMarketDataService
{
    Task<SchwabStockQuoteResponse> GetStockBySymbol(string symbol);
}