using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StockDashboard.API.Services;
using StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

namespace StockDashboard.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class StocksController(IStockService stockService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetStocks()
    {
        var stocks = stockService.GetStocks();
        return Ok(stocks);
    }
    
    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetStocksBySymbol([FromRoute]string symbol)
    {
        var stocks = await stockService.GetStockBySymbol(symbol);
        return Ok(stocks);
    }
    
    [HttpGet("/accounts")]
    public async Task<List<SchwabAccountResponse>> GetAccounts()
    {
        var accounts = await stockService.GetAccounts();
        return accounts;
    }
    
    [HttpGet("/accounts/encrypted")]
    public async Task<List<AccountNumberEncryptedResponse>> GetEncryptedAccounts()
    {
        var accounts = await stockService.GetEncryptedAccounts();
        return accounts;
    }
}