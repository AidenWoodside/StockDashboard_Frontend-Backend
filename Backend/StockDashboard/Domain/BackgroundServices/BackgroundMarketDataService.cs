using System.Diagnostics;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Providers.MarketData;

namespace StockDashboard.Domain.BackgroundServices;

public class BackgroundMarketDataService(
    IWebsocketFactory websocketFactory) 
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
        await websocketBase.Connect(stoppingToken);
        Console.WriteLine("Successfully established connection to websocket");
        
        //subscribe to stocks
        await websocketBase.SubscribeStock(_stocks, stoppingToken);
        Console.WriteLine("Successfully subscribed to stocks");

        //timer to regular updates to frontend
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Console.WriteLine("Waiting for quote/s...");
        while (!stoppingToken.IsCancellationRequested)
        {
            //receive async
            var quote = await websocketBase.ReceiveUpdate(stoppingToken);
            Console.WriteLine(quote);
            
            //publish to hub if 5 seconds elapsed
            if (stopwatch.ElapsedMilliseconds > 3500)
            {
                //reset timer
                stopwatch.Restart();
                
                //push to frontend websocket
                //does not need to be awaited since we want this loop to be as quick as possible
                //kick of send task, do no wait for response.
                SendStockUpdate(await websocketBase.MapResponseToDomainModel(quote));
            }
        }
        
        //Disconnect websocket
        websocketBase.DisconnectWebsocket();
    }

    private static void SendStockUpdate(List<Stock> stock)
    {
        Console.WriteLine("Sending stock update");
    }
}