namespace StockDashboard.Infrastructure.Configs;

public class SchwabProviderConfigs : BaseProviderConfigs
{
    public string AuthCode { get; set; }
    public string RefreshToken { get; set; }
}