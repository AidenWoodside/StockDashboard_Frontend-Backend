namespace StockDashboard.Infrastructure.Providers.MarketData;

public interface IWebsocketFactory
{
    public IWebsocketBase CreateWebsocket();
}