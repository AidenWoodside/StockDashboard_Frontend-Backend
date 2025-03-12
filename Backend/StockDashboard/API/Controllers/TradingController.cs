using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StockDashboard.API.Services;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class TradingController(ITradingService tradingService) : ControllerBase
{
    [HttpGet("/accounts")]
    public async Task<List<SchwabAccountResponse>> GetAccounts()
    {
        var accounts = await tradingService.GetAccounts();
        return accounts;
    }
    
    [HttpGet("/accounts/encrypted")]
    public async Task<List<AccountNumberEncryptedResponse>> GetEncryptedAccounts()
    {
        var accounts = await tradingService.GetEncryptedAccounts();
        return accounts;
    }
}