using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Models.BackgroundServiceModels;
using StockDashboard.Infrastructure.Providers.MarketData.Alpaca;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab;

public class SchwabWebsocket(IOptions<SchwabProviderConfigs> marketData, IMapper mapper) : WebsocketBase(marketData), ISchwabWebsocket
{
    public override async Task Connect(CancellationToken stoppingToken)
    {
        var configs = marketData.Value.marketData.websocketUrl;
        //set headers for websocket connection
        Ws.Options.SetRequestHeader("APCA-API-KEY-ID", base.Key);
        Ws.Options.SetRequestHeader("APCA-API-SECRET-KEY", base.Value);
        await base.Connect(stoppingToken);
    }

    public override async Task SubscribeStock(List<Stock> stocks, CancellationToken stoppingToken)
    {
        //prepare subscription message
        var byteMsg = JsonSerializer.SerializeToUtf8Bytes(new Subscribe
        {
            action = "subscribe",
            quotes = stocks.Select(t => t.Symbol).ToArray()
        });
        
        await base.SubscribeStock(byteMsg, stoppingToken);
        await base.ReceiveUpdate<string>(stoppingToken);
        await base.ReceiveUpdate<string>(stoppingToken);
        await base.ReceiveUpdate<string>(stoppingToken);
    }

    public new async Task<T> ReceiveUpdate<T>(CancellationToken stoppingToken)
    {
        var update = await base.ReceiveUpdate<QuoteResponse>(stoppingToken);
        var stocks = mapper.Map<T>(update);
        return stocks;
    }

    public Task UnsubscribeStock(string symbol)
    {
        throw new NotImplementedException();
    }
    
    public Task DisconnectWebsocket()
    {
        throw new NotImplementedException();
    }
}