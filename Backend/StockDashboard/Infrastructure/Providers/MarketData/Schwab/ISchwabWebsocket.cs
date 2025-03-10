﻿using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab;

public interface ISchwabWebsocket : IWebsocketBase
{
    Task<List<Stock>> ReceiveUpdate(CancellationToken stoppingToken);
}