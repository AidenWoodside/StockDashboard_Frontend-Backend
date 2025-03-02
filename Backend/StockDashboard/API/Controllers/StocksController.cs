using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StockDashboard.API.Services;

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
}