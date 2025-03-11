using System.Diagnostics;
using AutoMapper;
using Microsoft.Extensions.Options;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.Infrastructure.Providers.Trading.Schwab;

public class SchwabTokenProvider(HttpClient httpClient,
    IOptions<SchwabProviderConfigs> configs,
    IMapper mapper)
    : RestApiBase(httpClient), ISchwabTokenProvider
{
    private readonly string baseUrl = configs.Value.trading.BaseUrl;
    private Stopwatch stopwatch = new();
    private string accessToken = "";
    private string clientId =  Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(configs.Value.key));
    private string refreshToken = configs.Value.RefreshToken ?? "";
    private string credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{configs.Value.key}:{configs.Value.value}"));
    public async Task<string> GetAccessToken()
    {
        if (!stopwatch.IsRunning) 
            stopwatch.Start();
        
        //if accessToken exists and is not stale, return access token
        if (!accessToken.Equals("") && stopwatch.ElapsedMilliseconds < 1700000) 
            return accessToken;
        
        return await GetAccessTokenFromRefreshToken();
    }

    private async Task<string> GetAccessTokenFromRefreshToken()
    {
        var data = $"grant_type=refresh_token&refresh_token={refreshToken}";
        
        return await GetToken(data);
    }

    private async Task<string> GetToken(string request)
    {
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Basic {credentials}" },
        };
        
        var content = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken },
        };
        
        var accessTokenResponse = await PostFormUrlEncodedAsync<SchwabAccessTokenResponse>("https://api.schwabapi.com/v1/oauth/token",
            content,
            headers);

        accessToken = accessTokenResponse.access_token;
        refreshToken = accessTokenResponse.refresh_token;
        
        stopwatch.Restart();
        return accessToken;
    }
}