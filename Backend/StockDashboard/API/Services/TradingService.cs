using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.Trading.Schwab;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.API.Services;

public class TradingService( ISchwabTradingProvider schwabTradingProvider) : ITradingService
{


    public async Task<List<SchwabAccountResponse>> GetAccounts()
    {
        return await schwabTradingProvider.GetAccounts<List<SchwabAccountResponse>>();
    }
    
    public async Task<List<AccountNumberEncryptedResponse>> GetEncryptedAccounts()
    {
        return await schwabTradingProvider.GetEncryptedAccounts<List<AccountNumberEncryptedResponse>>();
    }
}