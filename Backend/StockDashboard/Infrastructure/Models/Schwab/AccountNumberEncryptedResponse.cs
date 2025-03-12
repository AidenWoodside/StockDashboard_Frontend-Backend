namespace StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

public class AccountNumberEncryptedResponse
{
    public string accountNumber { get; set; }
    public string hashValue { get; set; }
}