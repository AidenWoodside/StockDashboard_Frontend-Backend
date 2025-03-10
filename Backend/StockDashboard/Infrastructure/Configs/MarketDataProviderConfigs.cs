using StockDashboard.Infrastructure.Constants;

namespace StockDashboard.Infrastructure.Configs;

public class MarketDataProviderConfigs
{
    public WebsocketProviderConfig Websocket { get; set; }
    public ApiProviderConfig Api { get; set; }
}
public class WebsocketProviderConfig
{
    public string key { get; set; }
    public string value { get; set; }
    public string websocketUrl { get; set; }
}

public class ApiProviderConfig
{
}