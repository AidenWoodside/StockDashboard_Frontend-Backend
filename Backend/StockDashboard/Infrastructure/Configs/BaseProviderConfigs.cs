using StockDashboard.Infrastructure.Constants;

namespace StockDashboard.Infrastructure.Configs;

public class BaseProviderConfigs
{
    public string key { get; set; }
    public string value { get; set; }
    public MarketData marketData { get; set; }
    public Trading trading { get; set; }
}
public class MarketData
{
    public string websocketUrl { get; set; }
    public string BaseUrl { get; set; }
}

public class Trading
{
    public string BaseUrl { get; set; }
}