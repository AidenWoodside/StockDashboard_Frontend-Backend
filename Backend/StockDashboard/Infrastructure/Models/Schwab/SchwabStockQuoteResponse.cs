using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StockDashboard.Infrastructure.Providers.MarketData.Schwab.Models;

public class SchwabStockQuoteResponse
{
    public AssetMainType AssetMainType { get; set; }
    public AssetSubType AssetSubType { get; set; }
    public QuoteType? quoteType { get; set; }
    public bool realtime { get; set; }
    public int ssid { get; set; }
    public string symbol { get; set; }
    public ExtendedMarket extended { get; set; }
    public Fundamental fundamental { get; set; }
    public QuoteEquity quote { get; set; }
    public ReferenceEquity reference { get; set; }
    public RegularMarket regular { get; set; }
}

public class ExtendedMarket
{
    public decimal askPrice { get; set; }
    public int askSize { get; set; }
    public decimal bidPrice { get; set; }
    public int bidSize { get; set; }
    public decimal lastPrice { get; set; }
    public int lastSize { get; set; }
    public decimal mark { get; set; }
    public long quoteTime { get; set; }
    public int totalVolume { get; set; }
    public long tradeTime { get; set; }
}
public class Fundamental
{
    public decimal avg10DaysVolume { get; set; }
    public decimal avg1YearVolume { get; set; }
    public DateTime declarationDate { get; set; }
    public decimal divAmount { get; set; }
    public DateTime divExDate { get; set; }
    public int? divFreq { get; set; }
    public decimal divPayAmount { get; set; }
    public DateTime divPayDate { get; set; }
    public decimal divYield { get; set; }
    public decimal eps { get; set; }
    public decimal fundLeverageFactor { get; set; }
    public FundStrategy? fundStrategy { get; set; }
    public DateTime nextDivExDate { get; set; }
    public DateTime nextDivPayDate { get; set; }
    public decimal peRatio { get; set; }
}


public class QuoteEquity
{
    [JsonProperty("52WeekHigh")]
    public decimal WeekHigh52 { get; set; }
    [JsonProperty("52WeekLow")]
    public decimal WeekLow52 { get; set; }
    public string askMICId {get; set;}
    public decimal askPrice { get; set; }
    public int askSize { get; set; }
    public long askTime { get; set; }
    public string bidMICId { get; set; }
    public decimal bidPrice { get; set; }
    public int bidSize { get; set; }
    public long bidTime { get; set; }
    public decimal closePrice { get; set; }
    public decimal highPrice { get; set; }
    public string lastMICId { get; set; }
    public decimal lastPrice { get; set; }
    public int lastSize { get; set; }
    public decimal lowPrice { get; set; }
    public decimal mark { get; set; }
    public decimal markChange { get; set; }
    public decimal markPercentChange { get; set; }
    public decimal netChange { get; set; }
    public decimal netPercentChange { get; set; }
    public decimal openPrice { get; set; }
    public long quoteTime { get; set; }
    public string securityStatus { get; set; }
    public int totalVolume { get; set; }
    public long tradeTime { get; set; }
    public decimal volatility { get; set; }
}
public class ReferenceEquity
{
    public string cusip { get; set; }
    public string description { get; set; }
    public string exchange { get; set; }
    public string exchangeName { get; set; }
    public string fsiDesc { get; set; }
    public int htbQuantity { get; set; }
    public decimal htbRate { get; set; }
    public bool isHardToBorrow { get; set; }
    public bool isShortable { get; set; }
    public string otcMarketTier { get; set; }
}
public class RegularMarket
{
    public decimal regularMarketLastPrice { get; set; }
    public int regularMarketLastSize { get; set; }
    public decimal regularMarketNetChange { get; set; }
    public decimal regularMarketPercentChange { get; set; }
    public long regularMarketTradeTime { get; set; }
}

public enum FundStrategy
{
    A,
    L,
    P,
    Q,
    S
}

public enum AssetMainType
{
    BOND,
    EQUITY,
    FOREX,
    FUTURE,
    FUTURE_OPTION,
    INDEX,
    MUTUAL_FUND,
    OPTION
}

public enum AssetSubType
{
    COE,
    PRF,
    ADR,
    GDR,
    CEF,
    ETF,
    ETN,
    UIT,
    WAR,
    RGT
}

public enum QuoteType
{
    NBBO,
    NFL
}