import { CurrencyBalance } from './../../inventory/+models/currency-balance';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-currency-balance',
  templateUrl: './currency-balance.component.html',
  styleUrls: ['./currency-balance.component.css']
})
export class CurrencyBalanceComponent implements OnInit
{

  @Input()
  public balance: CurrencyBalance;

  constructor() { }

  ngOnInit()
  {
  }

}
