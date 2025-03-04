namespace StockDashboard.Infrastructure.Models.BackgroundServiceModels;

public class Subscribe
{
    public string action { get; set; }
    public string[] trades { get; set; }
    public string[] quotes { get; set; }
}