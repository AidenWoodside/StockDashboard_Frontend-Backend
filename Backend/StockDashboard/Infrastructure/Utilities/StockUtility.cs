using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers;
using StockDashboard.Infrastructure.Repositories;

namespace StockDashboard.Infrastructure.Utilities;

public class StockUtility(IStockRepository stockRepository, IMarketDataProvider marketDataProvider) : IStockUtility
{
    public IEnumerable<Stock> GetStocks()
    {
        return stockRepository.GetAllStocks();
    }

    public void UpdateStocks()
    {
        stockRepository.UpdateStockPrices();
    }
    
    public async Task<Stock> GetStockBySymbol(string symbol)
    {
        return await stockRepository.GetStockBySymbol(symbol);
    }
}