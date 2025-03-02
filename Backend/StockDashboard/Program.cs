using StockDashboard.API.Hubs;
using StockDashboard.API.Services;
using StockDashboard.API.Services.background;
using StockDashboard.Infrastructure.Providers;
using StockDashboard.Infrastructure.Repositories;
using StockDashboard.Infrastructure.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Application and Domain services
builder.Services.AddSingleton<IStockService, StockService>();

// Register the Infrastructure
builder.Services.AddSingleton<IStockRepository, StockRepository>();
builder.Services.AddSingleton<IStockUtility, StockUtility>();
builder.Services.AddTransient<IMarketDataProvider, MarketDataProvider>();
builder.Services.AddHttpClient<IMarketDataProvider, MarketDataProvider>();

// Register SignalR
builder.Services.AddSignalR();

// Register the background service that broadcasts stock updates
builder.Services.AddHostedService<StockTickerHostedService>();

//Add Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Register Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Configure CORS to allow your Angular app's URL
builder.Services.AddCors(options =>
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