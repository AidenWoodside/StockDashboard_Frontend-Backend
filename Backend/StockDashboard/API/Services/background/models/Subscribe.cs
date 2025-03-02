namespace StockDashboard.API.Services.background.models;

public class Subscribe
{
    public string action { get; set; }
    public string[] trades { get; set; }
    public string[] quotes { get; set; }
}