using AutoMapper;
using Microsoft.Extensions.Options;
using StockDashboard.Domain.Models;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab.Models;
using StockDashboard.Infrastructure.Providers.Trading.Schwab;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab;

public class SchwabMarketDataProvider(IOptions<SchwabProviderConfigs> configs,
    ISchwabTokenProvider schwabTokenProvider,
    HttpClient httpClient,
    IMapper mapper) : RestApiBase(httpClient), ISchwabMarketDataProvider
{
    private string baseUrl = configs.Value.marketData.BaseUrl;
    public async Task<SchwabStockQuoteResponse> GetStockBySymbol(string symbol)
    {
        var response = await GetAsync<Dictionary<string,SchwabStockQuoteResponse>>(baseUrl + $"/{symbol}/quotes", await AddHeaders());
        return response[symbol];
    }
    
    private async Task<Dictionary<string, string>> AddHeaders()
    {
        return new Dictionary<string, string>
        {
            { "Accept", "application/json" },
            { "Authorization", $"Bearer {await schwabTokenProvider.GetAccessToken()}" }
        };
    }
}