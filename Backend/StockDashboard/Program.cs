using StockDashboard.API.Controllers;
using StockDashboard.API.Services;
using StockDashboard.Domain.BackgroundServices;
using StockDashboard.Domain.Hubs;
using StockDashboard.Infrastructure.Configs;
using StockDashboard.Infrastructure.Constants;
using StockDashboard.Infrastructure.Models;
using StockDashboard.Infrastructure.Providers.MarketData;
using StockDashboard.Infrastructure.Providers.MarketData.Alpaca;
using StockDashboard.Infrastructure.Providers.MarketData.Schwab;
using StockDashboard.Infrastructure.Providers.Trading.Schwab;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

//Set Configs


services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddHttpClient();

//Register Providers
services.Configure<CurrentWebsocketProviderConfigs>(builder.Configuration.GetSection("Providers:CurrentWebsocketProviderConfigs"));
services.AddSingleton<IWebsocketFactory, WebsocketFactory>();

services.Configure<AlpacaProviderConfigs>(builder.Configuration.GetRequiredSection("Providers:Alpaca"));
services.AddSingleton<AlpacaWebsocket>();

services.Configure<SchwabProviderConfigs>(builder.Configuration.GetSection("Providers:Schwab"));
services.AddSingleton<SchwabWebsocket>();

services.AddScoped<IMarketDataService, MarketDataService>();

services.AddScoped<IAlpacaMarketDataProvider, AlpacaMarketDataProvider>();

services.AddScoped<ISchwabMarketDataProvider, SchwabMarketDataProvider>();

services.AddScoped<IWebsocketBase>(provider => provider.GetRequiredService<ISchwabWebsocket>());

services.AddScoped<ISchwabTokenProvider, SchwabTokenProvider>();
services.AddScoped<ISchwabTradingProvider, SchwabTradingProvider>();

// Register Application and Domain services
services.AddScoped<ITradingService, TradingService>();

services.AddScoped<ISchwabTradingProvider, SchwabTradingProvider>();

// Register SignalR
services.AddSignalR();

// Register the background service that broadcasts stock updates
services.AddHostedService<BackgroundMarketDataService>();

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
if (!app.Environment.IsDevelopment())
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