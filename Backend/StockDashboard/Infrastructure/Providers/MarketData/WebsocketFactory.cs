using StockDashboard.Infrastructure.Providers.MarketData.Alpaca;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public class WebsocketFactory(
    IServiceProvider serviceProvider, 
    IConfiguration configuration) : IWebsocketFactory
{
    public IWebsocketBase CreateWebsocket()
    {
        // Read the provider setting from configuration
        string provider = configuration["Providers:CurrentDataProvider"]
            ?? throw new NullReferenceException("Providers:CurrentDataProvider");

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