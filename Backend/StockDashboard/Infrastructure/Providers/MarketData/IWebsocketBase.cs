using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public interface IWebsocketBase
{
    public Task Connect(CancellationToken cancellationToken);

    public Task<string> ReceiveUpdate(CancellationToken stoppingToken);
    public Task DisconnectWebsocket();
    
    public Task SubscribeStock(byte[] byteMsg, CancellationToken cancellationToken);
    
    public Task UnsubscribeStock(string symbol);
    
    public Task SubscribeStock(List<Stock> stocks, CancellationToken cancellationToken);

    public Task<List<Stock>> MapResponseToDomainModel(string response);
}