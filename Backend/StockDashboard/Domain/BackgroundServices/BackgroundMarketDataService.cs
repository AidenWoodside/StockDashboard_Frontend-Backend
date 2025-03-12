using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using StockDashboard.Domain.Hubs;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.MarketData;

namespace StockDashboard.Domain.BackgroundServices;

public class BackgroundMarketDataService(
    IWebsocketFactory websocketFactory,
    IHubContext<StockHub> hubContext) 
    : BackgroundService
{
    
    private List<Stock> _stocks = new()
    {
        new Stock
        {
            Symbol = "FAKEPACA"
        }
    };
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var websocketBase = websocketFactory.CreateWebsocket();
        
        //Connect websocket
        Console.WriteLine("Start connection to websocket");
        await websocketBase.Connect(stoppingToken);
        Console.WriteLine("Successfully established connection to websocket");
        
        //subscribe to stocks
        Console.WriteLine("Start subscribe to stocks");
        await websocketBase.SubscribeStock(_stocks, stoppingToken);
        Console.WriteLine("Successfully subscribed to stocks");

        //timer to regular updates to frontend
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Console.WriteLine("Waiting for quote/s...");
        while (!stoppingToken.IsCancellationRequested)
        {
            //receive async
            var quote = await websocketBase.ReceiveUpdate<List<Stock>>(stoppingToken);
            var output = new StringBuilder();
            foreach (var stock in quote) output.Append($"Symbol: {stock.Symbol} Price: {stock.Price} ");
            
            Console.WriteLine(output.ToString());
            
            //publish to hub if 5 seconds elapsed
            if (stopwatch.ElapsedMilliseconds > 3500)
            {
                //reset timer
                stopwatch.Restart();
                
                //push to frontend websocket
                //does not need to be awaited since we want this loop to be as quick as possible
                //kick of send task, do no wait for response.
                SendStockUpdate(quote, stoppingToken);
            }
        }
        
        //Disconnect websocket
        websocketBase.DisconnectWebsocket();
    }

    private void SendStockUpdate(List<Stock> stock, CancellationToken stoppingToken)
    {
        var random = new Random();
        stock[0].Price = random.Next();
        hubContext.Clients.All.SendAsync("ReceiveStockUpdateQuote", stock.ToArray(), cancellationToken: stoppingToken);
    }
}