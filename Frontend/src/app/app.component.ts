import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {StockDisplayComponent} from './Components/stock-display/stock-display.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, StockDisplayComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'StockDash';
}
