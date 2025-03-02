using System;
using System.Threading.Tasks;
using StockDashboard.Domain.Models;
using Environments = Alpaca.Markets.Environments;

namespace StockDashboard.Infrastructure.Providers;

public class MarketDataProvider(HttpClient httpClient):IMarketDataProvider
{
    private string QueryUrl = "http://www.randomnumberapi.com/api/v1.0/";
    
    public async Task<Stock> GetStocksAsync(string symbol)
    {
        var relativeUrl = $"{QueryUrl}random?min=100&max=1000&count=5";
        var response = await httpClient.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<int[]>();
        return new Stock()
        {
            Symbol = symbol,
            Price = result[0]
        };
    }
}