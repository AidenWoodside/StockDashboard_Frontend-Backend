namespace StockDashboard.Infrastructure.Models.BackgroundServiceModels;

public class TradingResponse
{
    public string T { get; set; }
    public string S { get;  set;}
    
    public int i { get;  set;}
    
    public string x { get;  set;}
    
    public decimal p { get;  set;}
    
    public int s { get;  set;}
    
    public string[] c { get;  set;}
    
    public string t { get;  set;}
    
    public string z { get; set; }
}

public class QuoteResponse
{
    public string T { get; set; }
    public string S { get;  set;}
    
    public string ax { get;  set;}
    
    public decimal ap { get;  set;}
    
    public int @as { get;  set;}
    
    public string bx { get;  set;}
    
    public decimal bp { get;  set;}
    
    public int bs { get;  set;}
    
    public string[] c { get; set; }
    
    public string t { get; set; }
    
    public string z { get; set; }
}

public class MarketData()
{
    public TradingResponse[] tradingResponse { get; set; }
    public QuoteResponse[] quoteResponse { get; set; }
}
/*
 *
 *  public string T { get; set; }
    public string S { get;  set;}
    public string ax { get;  set;}
    public decimal ap { get;  set;}
    //public int @as { get; }
    public string bx { get;  set;}
    public decimal bp { get;  set;}
    public int bs { get;  set;}
    public string[] c { get;  set;}
    public string t { get;  set;}
    public string z { get; set; }
 */