using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.API.Services;

public interface IStockService
{
    IEnumerable<Stock> GetStocks();
    Task<Stock> GetStockBySymbol(string symbol);
    
    Task<List<SchwabAccountResponse>> GetAccounts();
    
    Task<List<AccountNumberEncryptedResponse>> GetEncryptedAccounts();
}