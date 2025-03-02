using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Repositories;

public interface IStockRepository
{
        IEnumerable<Stock> GetAllStocks();
        void UpdateStockPrices();
        Task<Stock> GetStockBySymbol(string symbol);
}