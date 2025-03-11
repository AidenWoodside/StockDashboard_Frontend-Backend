using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.Trading.Schwab;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;
using StockDashboard.Infrastructure.Repositories;
using StockDashboard.Infrastructure.Utilities;

namespace StockDashboard.API.Services;

public class StockService(IStockUtility stockUtility, ISchwabTradingProvider schwabTradingProvider) : IStockService
{
    public IEnumerable<Stock> GetStocks() => stockUtility.GetStocks();
    
    public async Task<Stock> GetStockBySymbol(string symbol)
    {
        return await stockUtility.GetStockBySymbol(symbol);
    }

    public async Task<List<SchwabAccountResponse>> GetAccounts()
    {
        return await schwabTradingProvider.GetAccounts<List<SchwabAccountResponse>>();
    }
    
    public async Task<List<AccountNumberEncryptedResponse>> GetEncryptedAccounts()
    {
        return await schwabTradingProvider.GetEncryptedAccounts<List<AccountNumberEncryptedResponse>>();
    }
}