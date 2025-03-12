namespace StockDashboard.Infrastructure.Providers.Trading.Schwab.Models;

public class SchwabAccountResponse
{
    public SecuritiesAccount securitiesAccount { get; set; }
}

public class SecuritiesAccount
{
    public MarginInitialBalance initialBalances { get; set; }
    public MarginBalance currentBalance { get; set; }
    public MarginBalance projectedBalances { get; set; }
    public string type { get; set; }
    public string accountNumber { get; set; }
    public int roundTrips { get; set; }
    public bool isDayTrader { get; set; }
    public bool isClosingOnlyRestricted { get; set; }
    public bool pfcbFlag { get; set; }
    public Position[] positions { get; set; }
}

public class MarginInitialBalance
{
    public decimal accruedInterest { get; set; }
    public decimal availableFundsNonMarginableTrade { get; set; }
    public decimal bondValue { get; set; }
    public decimal buyingPower { get; set; }
    public decimal cashBalance { get; set; }
    public decimal cashAvailableForTrading { get; set; }
    public decimal cashReceipts { get; set; }
    public decimal dayTradingBuyingPower { get; set; }
    public decimal dayTradingBuyingPowerCall { get; set; }
    public decimal dayTradingEquityCall { get; set; }
    public decimal equity { get; set; }
    public decimal equityPercentage { get; set; }
    public decimal liquidationValue { get; set; }
    public decimal longMarginValue { get; set; }
    public decimal longOptionMarketValue { get; set; }
    public decimal longStockValue { get; set; }
    public decimal maintenanceCall { get; set; }
    public decimal maintenanceRequirement { get; set; }
    public decimal margin { get; set; }
    public decimal marginEquity { get; set; }
    public decimal moneyMarketFund { get; set; }
    public decimal mutualFundValue { get; set; }
    public decimal regTCall { get; set; }
    public decimal shortMarginValue { get; set; }
    public decimal shortOptionMarketValue { get; set; }
    public decimal shortStockValue { get; set; }
    public decimal totalCash { get; set; }
    public bool isInCall { get; set; }
    public decimal unsettledCash { get; set; }
    public decimal pendingDeposits { get; set; }
    public decimal marginBalance { get; set; }
    public decimal shortBalance { get; set; }
    public decimal accountValue { get; set; }
}

public class MarginBalance
{
    public decimal availableFunds { get; set; }
    public decimal availableFundsNonMarginableTrade { get; set; }
    public decimal buyingPower { get; set; }
    public decimal buyingPowerNonMarginableTrade { get; set; }
    public decimal dayTradingBuyingPower { get; set; }
    public decimal dayTradingBuyingPowerCall { get; set; }
    public decimal equity { get; set; }
    public decimal equityPercentage { get; set; }
    public decimal longMarginValue { get; set; }
    public decimal maintenanceCall { get; set; }
    public decimal maintenanceRequirement { get; set; }
    public decimal marginBalance { get; set; }
    public decimal regTCall { get; set; }
    public decimal shortBalance { get; set; }
    public decimal shortMarginValue { get; set; }
    public decimal sma { get; set; }
    public bool isInCall { get; set; }
    public decimal stockBuyingPower { get; set; }
    public decimal optionBuyingPower { get; set; }
}

public class Position
{
    public decimal shortQuantity { get; set; }
    public decimal averagePrice { get; set; }
    public decimal currentDayProfitLoss { get; set; }
    public decimal currentDayProfitLossPercentage { get; set; }
    public decimal longQuantity { get; set; }
    public decimal settledLongQuantity { get; set; }
    public decimal settledShortQuantity { get; set; }
    public decimal agedQuantity { get; set; }
    public AccountsInstrument instrument { get; set; }
    public decimal marketValue { get; set; }
    public decimal maintenanceRequirement { get; set; }
    public decimal averageLongPrice { get; set; }
    public decimal averageShortPrice { get; set; }
    public decimal taxLotAverageLongPrice { get; set; }
    public decimal taxLotAverageShortPrice { get; set; }
    public decimal longOpenProfitLoss { get; set; }
    public decimal shortOpenProfitLoss { get; set; }
    public decimal previousSessionLongQuantity { get; set; }
    public decimal previousSessionShortQuantity { get; set; }
    public decimal currentDayCost { get; set; }
}

public class AccountsInstrument
{
    public string type { get; set; }
    public string assetType { get; set; }
    public string cusip { get; set; }
    public string symbol { get; set; }
    public string description { get; set; }
    public int instrumentId { get; set; }
    public decimal netChange { get; set; }
}