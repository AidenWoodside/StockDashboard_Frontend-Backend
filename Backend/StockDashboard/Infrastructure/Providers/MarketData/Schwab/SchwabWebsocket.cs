using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Models.BackgroundServiceModels;
using StockDashboard.Infrastructure.Providers.MarketData.Alpaca;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab;

public class SchwabWebsocket(IOptions<SchwabMarketDataProviderConfigs> marketData, IMapper mapper) : WebsocketBase(marketData), ISchwabWebsocket
{
    public override async Task Connect(CancellationToken stoppingToken)
    {
        var configs = marketData.Value.Websocket;
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
        await base.ReceiveUpdate(stoppingToken);
        await base.ReceiveUpdate(stoppingToken);
        await base.ReceiveUpdate(stoppingToken);
    }

    public new async Task<List<Stock>> ReceiveUpdate(CancellationToken stoppingToken)
    {
        var update = await base.ReceiveUpdate(stoppingToken);
        var stocks = JsonSerializer.Deserialize<List<Stock>>(update);
        return stocks;
    }

    public override async Task<List<Stock>> MapResponseToDomainModel(string response)
    {
        var test2 = JToken.Parse(response);
        var test3 = test2.GroupBy(token => token["T"]?.ToString())
            .ToDictionary(g => g.Key, g => new JArray(g));

        var model = new Models.BackgroundServiceModels.MarketData();

        if (test3.TryGetValue("q", out var quotes))
        {
            model.quoteResponse = mapper.Map<QuoteResponse[]>(quotes);
            
        }
        
        return mapper.Map<List<Stock>>(model.quoteResponse);;
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