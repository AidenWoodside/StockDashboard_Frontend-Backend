// src/app/components/stock-display/stock-display.component.ts
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { StockDataService } from '../../Services/stock-data.service';
import {Stock, StockDataModel} from '../../Models/stock-data-model';

@Component({
  selector: 'app-stock-display',
  templateUrl: './stock-display.component.html',
  styleUrls: ['./stock-display.component.css'],
  imports: [CommonModule]
})
export class StockDisplayComponent implements OnInit {
  // Local property to store the array of stocks received from the service.
  myDictionary = new Map<string, number>();

  constructor(private stockDataService: StockDataService) { }
  getDictSize(): number {
    return this.myDictionary.size;
  }

  get mapEntries() {
    return Array.from(this.myDictionary.entries()); // Convert Map to Array
  }
  ngOnInit(): void {
    // Start the SignalR connection.
    this.stockDataService.startConnection();

    // Subscribe to the stockData$ observable which now emits an array of Stock objects.
    this.stockDataService.stockData$.subscribe((stocks: Stock[]) => {
      for(let i = 0; i < stocks.length; i++){
        this.myDictionary.set(stocks[i].symbol, stocks[i].price);
      }
      console.log(this.myDictionary)
    });
  }
}
