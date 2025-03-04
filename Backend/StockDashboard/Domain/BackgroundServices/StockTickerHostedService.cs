using System.Net.WebSockets;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using StockDashboard.Domain.Hubs;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Models.BackgroundServiceModels;
using StockDashboard.Infrastructure.Utilities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace StockDashboard.Domain.BackgroundServices;

public class StockTickerHostedService(
    IHubContext<StockHub> hubContext,
    IStockUtility stockUtility,
    ILogger<StockTickerHostedService> _logger,
    IMapper mapper)
    : BackgroundService
{
    //private readonly string key = "PKJRUWSCO8KM1SRS94VY";
    //private readonly string value = "cnCdaAI2ylogvKcBZ4KpbFPcDIs6GFbcfyx1nGXP";
    
    private readonly string key = "AKLOGKWIQ77QVQJTNJQH";
    private readonly string value = "GnbNGiPL8j5GkRx96XCkfLWXHQ8HuegaaS69IcvF";
    
    private readonly List<string> stockSymbols = new()
    {
        "FAKEPACA","AAPL","GOOGL","NFLX","TSLA",
        "TV", "TW", "UFO", "UFCS", "UGA", "TXT",
        "TYG", "TY", "TYL", "SA", "SAFE", "SAH"
    };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var uri = new Uri("wss://stream.data.alpaca.markets/v2/test");
        using var ws = new ClientWebSocket();
        ws.Options.SetRequestHeader("APCA-API-KEY-ID", key);
        ws.Options.SetRequestHeader("APCA-API-SECRET-KEY", value);
        
        await ws.ConnectAsync(uri, stoppingToken);

        var byteMsg = JsonSerializer.SerializeToUtf8Bytes(new Subscribe
            {
                action = "subscribe",
                quotes = stockSymbols.ToArray()
            });
        
        await ws.SendAsync(byteMsg, WebSocketMessageType.Text, true, stoppingToken);
        var buffer = new byte[4096];
        await ws.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);
        await ws.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);
        await ws.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);
                var resMsg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                //var test = resMsg.Substring(1, resMsg.Length - 2);
                _logger.LogInformation(resMsg);

                var test2 = JToken.Parse(resMsg);
                var test3 = test2.GroupBy(token => token["T"]?.ToString())
                    .ToDictionary(g => g.Key, g => new JArray(g));

                var model = new MarketData();

                if (test3.TryGetValue("q", out var quotes))
                {
                    model.quoteResponse = mapper.Map<QuoteResponse[]>(quotes);
                    var ma = mapper.Map<Stock[]>(model.quoteResponse);
                    hubContext.Clients.All.SendAsync("ReceiveStockUpdateQuote", ma, cancellationToken: stoppingToken);
                }
                
                if (test3.TryGetValue("t", out var trades))
                {
                    model.tradingResponse = mapper.Map<TradingResponse[]>(trades);
                    var ma = mapper.Map<Stock[]>(model.tradingResponse);
                    hubContext.Clients.All.SendAsync("ReceiveStockUpdateTrade", ma, cancellationToken: stoppingToken);
                }
                
                
                
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    _logger.LogInformation("WebSocket closed by the remote host.");
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", stoppingToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            
        }
    }

    /*protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Update stock prices using the application service
            stockUtility.UpdateStocks();

            // Retrieve the updated stock list
            var stocks = stockUtility.GetStocks();
            Console.WriteLine("Test Log");
            // Broadcast the updated stock data to all connected clients
            await hubContext.Clients.All.SendAsync("ReceiveStockUpdate", stocks, cancellationToken: stoppingToken);
            // Wait one second between updates
            await Task.Delay(1000, stoppingToken);
        }
    }*/

    /*protected override async Task ExecuteAsyncs(CancellationToken stoppingToken)
    {

        var dataStreamingClient = Environments.Paper.GetAlpacaDataStreamingClient(
            new SecretKey("PKJRUWSCO8KM1SRS94VY","cnCdaAI2ylogvKcBZ4KpbFPcDIs6GFbcfyx1nGXP"));
        var test = await dataStreamingClient.ConnectAndAuthenticateAsync(stoppingToken);
        
        
        var subscriber = dataStreamingClient.GetTradeSubscription("FAKEPACA");
        var sub = dataStreamingClient.GetQuoteSubscription("FAKEPACA");

        subscriber.Received += async trade =>
        {
            var stocks = new Stock
            {
                Symbol = trade.Symbol,
                Price = trade.Price
            };
            await hubContext.Clients.All.SendAsync("ReceiveStockUpdate", new []{stocks}, cancellationToken: stoppingToken);
            Console.WriteLine(trade.Price + " " + trade.Symbol);
        };
        subscriber.OnSubscribedChanged += () =>
        {
            Console.WriteLine("OnSubscribedChanged");
        };
        
        
        
        await dataStreamingClient.SubscribeAsync(subscriber, stoppingToken);
        await dataStreamingClient.SubscribeAsync(sub, stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("TESTS");
        }
    }*/
}
