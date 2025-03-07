using StockDashboard.API.Services;
using StockDashboard.Domain.BackgroundServices;
using StockDashboard.Domain.Hubs;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Models;
using StockDashboard.Infrastructure.Providers.MarketData;
using StockDashboard.Infrastructure.Providers.MarketData.Alpaca;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab;
using StockDashboard.Infrastructure.Providers.Trading.Schwab;
using StockDashboard.Infrastructure.Repositories;
using StockDashboard.Infrastructure.Utilities;

var builder = WebApplication.CreateBuilder(args);

var services = WebApplication.CreateBuilder(args).Services;

//Set Configs


services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Application and Domain services
services.AddSingleton<IStockService, StockService>();

// Register the Infrastructure
services.AddSingleton<IStockRepository, StockRepository>();
services.AddSingleton<IStockUtility, StockUtility>();

//Register Providers
switch (builder.Configuration.GetValue<string>("MarketDataProvider"))
{
    case "Alpaca":
        services.Configure<MarketDataProviderConfigs>(builder.Configuration.GetSection("MarketDataProvider"));
        services.AddScoped<IAlpacaMarketDataProvider, AlpacaMarketDataProvider>();
        services.AddScoped<IMarketDataProvider>(provider => provider.GetRequiredService<IAlpacaMarketDataProvider>());
        break;
    case "Schwab":
        services.Configure<MarketDataProviderConfigs>(builder.Configuration.GetSection("MarketDataProvider"));
        services.AddScoped<ISchwabMarketDataProvider, SchwabMarketDataProvider>();
        services.AddScoped<IMarketDataProvider>(provider => provider.GetRequiredService<ISchwabMarketDataProvider>());
        break;
    default:
        throw new Exception($"Unknown MarketDataProvider: {builder.Configuration.GetValue<string>("MarketDataProvider")}");
}

services.AddScoped<ISchwabTradingProvider, SchwabTradingProvider>();

// Register SignalR
services.AddSignalR();

// Register the background service that broadcasts stock updates
services.AddHostedService<StockTickerHostedService>();

//Add Controllers
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Register Swagger/OpenAPI services
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


// Configure CORS to allow your Angular app's URL
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular dev server URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline for Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // You can customize the Swagger UI endpoint if desired.
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

// Map controller endpoints
app.MapControllers();

app.UseCors("CorsPolicy");
app.MapHub<StockHub>("/stockhub");

app.Run();