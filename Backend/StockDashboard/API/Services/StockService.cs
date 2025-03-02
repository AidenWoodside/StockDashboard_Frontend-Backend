using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Repositories;
using StockDashboard.Infrastructure.Utilities;

namespace StockDashboard.API.Services;

public class StockService(IStockUtility stockUtility) : IStockService
{
    public IEnumerable<Stock> GetStocks() => stockUtility.GetStocks();
    
    public async Task<Stock> GetStockBySymbol(string symbol)
    {
        return await stockUtility.GetStockBySymbol(symbol);
    }
}