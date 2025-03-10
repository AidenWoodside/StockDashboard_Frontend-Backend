﻿using System.Net.WebSockets;
using System.Text;
using Microsoft.Extensions.Options;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Configs;

namespace StockDashboard.Infrastructure.Providers.MarketData;

public class WebsocketBase(IOptions<MarketDataProviderConfigs> websocketConfigs) : IWebsocketBase
{
    protected readonly string Key = websocketConfigs.Value.Websocket.key;
    protected readonly string Value = websocketConfigs.Value.Websocket.value;
    private readonly string _websocketUrl =  websocketConfigs.Value.Websocket.websocketUrl;
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

    public virtual async Task<string> ReceiveUpdate(CancellationToken stoppingToken)
    {
        var result = await Ws.ReceiveAsync(new ArraySegment<byte>(_buffer), stoppingToken);
        var resMsg = Encoding.UTF8.GetString(_buffer, 0, result.Count);
        return resMsg;
    }

    public Task DisconnectWebsocket()
    {
        throw new NotImplementedException();
    }


    public Task UnsubscribeStock(string symbol)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<List<Stock>> MapResponseToDomainModel(string response)
    {
        throw new NotImplementedException();
    }

    public virtual async Task SubscribeStock(List<Stock> stocks, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
}