using Microsoft.AspNetCore.Mvc;
using StockDashboard.API.Services;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab.Models;

namespace StockDashboard.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class MarketDataController(IMarketDataService marketDataService) : ControllerBase
{
    
    [HttpGet("{symbol}")]
    public async Task<SchwabStockQuoteResponse> GetStockBySymbol([FromRoute]string symbol)
    {
        symbol = symbol.ToUpper();
        var stocks = await marketDataService.GetStockBySymbol(symbol);
        return stocks;
    }
}