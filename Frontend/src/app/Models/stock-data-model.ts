export interface Stock {
  symbol: string;
  price: number;
}

export interface StockDataModel {
  // 'arguments' is an array with one element that is an array of Stock items.
  stocks: Stock[];
}
