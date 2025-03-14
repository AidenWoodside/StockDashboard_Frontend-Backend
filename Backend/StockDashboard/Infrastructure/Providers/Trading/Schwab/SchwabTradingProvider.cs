﻿
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.Options;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.Infrastructure.Providers.Trading.Schwab;

public class SchwabTradingProvider(HttpClient httpClient,
    IOptions<SchwabProviderConfigs> configs,
    IMapper mapper,
    ISchwabTokenProvider schwabTokenProvider)
    : RestApiBase(httpClient), ISchwabTradingProvider
{
    private readonly string baseUrl = configs.Value.trading.BaseUrl;

    public string PlaceOrder()
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetAccounts<T>()
    {
        var response = await GetAsync<T>(baseUrl+ "/accounts", await AddHeaders())
            ?? throw new NullReferenceException("GetAccounts() returned null");
        
        return mapper.Map<T>(response);
    }
    
    public async Task<T> GetEncryptedAccounts<T>()
    {
        var response = await GetAsync<T>(baseUrl+ "/accounts/accountNumbers", await AddHeaders())
                       ?? throw new NullReferenceException("GetAccounts() returned null");
        
        return mapper.Map<T>(response);
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