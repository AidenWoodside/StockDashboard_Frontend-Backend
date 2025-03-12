using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.API.Services;

public interface ITradingService
{
    Task<List<SchwabAccountResponse>> GetAccounts();
    
    Task<List<AccountNumberEncryptedResponse>> GetEncryptedAccounts();
}