using StockDashboard.Domain.Models;

namespace StockDashboard.API.Services;

public interface IStockService
{
    IEnumerable<Stock> GetStocks();
    Task<Stock> GetStockBySymbol(string symbol);
}