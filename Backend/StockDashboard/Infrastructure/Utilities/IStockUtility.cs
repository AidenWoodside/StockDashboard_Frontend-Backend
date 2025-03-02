using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Utilities;

public interface IStockUtility
{
    IEnumerable<Stock> GetStocks();
    void UpdateStocks();
    
    Task<Stock> GetStockBySymbol(string symbol);
}