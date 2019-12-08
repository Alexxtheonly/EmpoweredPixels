import { CurrencyBalance } from './../../../inventory/+models/currency-balance';
import { ShopItemPrice } from './../../+models/shop-item-price';
import { Component, OnInit, Input, Output } from '@angular/core';

@Component({
  selector: 'app-shop-item-price',
  templateUrl: './shop-item-price.component.html',
  styleUrls: ['./shop-item-price.component.css']
})
export class ShopItemPriceComponent implements OnInit
{

  @Input()
  public prices: ShopItemPrice[];

  @Input()
  public balances: CurrencyBalance[];

  constructor() { }

  ngOnInit()
  {
  }

  public hasSufficientBalance(price: ShopItemPrice): boolean
  {
    if (!this.balances)
    {
      return;
    }

    const balance = this.balances.find(o => o.itemId === price.currencyItemId);
    if (!balance)
    {
      return false;
    }

    return balance.balance >= price.quantity;
  }

  public canBeBought(): boolean
  {
    if (!this.prices)
    {
      return false;
    }

    for (const price of this.prices)
    {
      if (!this.hasSufficientBalance(price))
      {
        return false;
      }

      return true;
    }
  }

}
