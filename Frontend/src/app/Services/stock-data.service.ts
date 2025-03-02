// src/app/services/stock-data.service.ts
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { Stock, StockDataModel } from '../Models/stock-data-model';

@Injectable({
  providedIn: 'root'
})
export class StockDataService {
  // Using the definite assignment assertion (!) because the connection will be initialized in startConnection()
  private hubConnection!: signalR.HubConnection;
  // This subject will emit an array of Stock objects extracted from the backend message.
  public stockData$: Subject<Stock[]> = new Subject();

  constructor() { }

  startConnection(): void {
    // Build the SignalR hub connection using the URL specified in your environment configuration.
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5041/stockhub")
      .build();

    // Start the SignalR connection.
    this.hubConnection.start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.error('Error while starting SignalR connection: ', err));

    // Listen for the "ReceiveStockUpdate" event.
    this.hubConnection.on('ReceiveStockUpdateQuote', (message: Stock[]) => {
      console.log(message)
      // Emit the extracted stocks array to any subscribers.
      this.stockData$.next(message);
    });

    this.hubConnection.on('ReceiveStockUpdateTrade', (message: Stock[]) => {
      console.log(message)
      // Emit the extracted stocks array to any subscribers.
      this.stockData$.next(message);
    });
  }
}
