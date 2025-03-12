using System.Net.WebSockets;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Configs;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public abstract class WebsocketBase(IOptions<BaseProviderConfigs> websocketConfigs) : IWebsocketBase
{
    protected readonly string Key = websocketConfigs.Value.key;
    protected readonly string Value = websocketConfigs.Value.value;
    private readonly string _websocketUrl =  websocketConfigs.Value.marketData.websocketUrl;
    private byte[] _buffer = new byte[4096];
    protected ClientWebSocket Ws = new();
    
    public virtual async Task Connect(CancellationToken stoppingToken)
    {
        await Ws.ConnectAsync(new Uri(_websocketUrl), stoppingToken);
    }
    public virtual async Task SubscribeStock(byte[] byteMsg, CancellationToken stoppingToken)
    {
        await Ws.SendAsync(byteMsg, WebSocketMessageType.Text, true, stoppingToken);
    }

    public virtual async Task<T> ReceiveUpdate<T>(CancellationToken stoppingToken)
    {
        var result = await Ws.ReceiveAsync(new ArraySegment<byte>(_buffer), stoppingToken);
        var resMsg = Encoding.UTF8.GetString(_buffer, 0, result.Count);
        
        if (typeof(T) == typeof(string))
            return (T)(object)resMsg;
        
        var mapped = JsonConvert.DeserializeObject<T>(resMsg);
        return mapped;
    }

    public Task DisconnectWebsocket()
    {
        throw new NotImplementedException();
    }

    public Task UnsubscribeStock(string symbol)
    {
        throw new NotImplementedException();
    }
    
    public abstract Task SubscribeStock(List<Stock> byteMsg, CancellationToken stoppingToken);
}