using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public interface IWebsocketBase
{
    public Task Connect(CancellationToken cancellationToken);

    public Task<T> ReceiveUpdate<T>(CancellationToken stoppingToken);
    public Task DisconnectWebsocket();
    
    public Task SubscribeStock(byte[] byteMsg, CancellationToken cancellationToken);
    
    public Task UnsubscribeStock(string symbol);
    
    Task SubscribeStock(List<Stock> byteMsg, CancellationToken stoppingToken);
    
}