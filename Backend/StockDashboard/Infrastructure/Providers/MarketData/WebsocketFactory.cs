using Microsoft.Extensions.Options;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Providers.MarketData.Alpaca;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public class WebsocketFactory(
    IServiceProvider serviceProvider, 
    IOptions<CurrentWebsocketProviderConfigs> currentWebsocketProvider) : IWebsocketFactory
{
    public IWebsocketBase CreateWebsocket()
    {
        var provider = currentWebsocketProvider.Value.CurrentWebsocketProvider.ToString();
        switch (provider)
        {
            case "Alpaca":
                // Retrieve the Alpaca implementation from the DI container
                return serviceProvider.GetRequiredService<AlpacaWebsocket>();
            case "Schwab":
                // Retrieve the Schwab implementation from the DI container
                return serviceProvider.GetRequiredService<SchwabWebsocket>();
            default:
                throw new InvalidOperationException($"Invalid websocket provider specified: {provider}");
        }
    }
}