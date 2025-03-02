using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly List<Stock> _stocks =
    [
        new() { Symbol = "AAPL", Price = 150.00m },
        new() { Symbol = "MSFT", Price = 250.00m },
        new() { Symbol = "AMZN", Price = 3300.00m },
        new() { Symbol = "GOOGL", Price = 2800.00m },
        new() { Symbol = "TSLA", Price = 700.00m }
    ];

    public IEnumerable<Stock> GetAllStocks() => _stocks;

    public async Task<Stock> GetStockBySymbol(string symbol)
    {
        var task = Task.Run(() =>
        {
            return _stocks.Find(s => s.Symbol == symbol) ?? throw new InvalidOperationException("No Matching Symbol");
        });
        return await task;
    }
    
    public void UpdateStockPrices()
    {
        var random = new Random();
        foreach (var stock in _stocks)
        {
            // Simulate a change between -1 and +1
            var change = (decimal)(random.NextDouble() * 2 - 1);
            stock.Price = Math.Round(stock.Price + change, 2);
        }
    }
}