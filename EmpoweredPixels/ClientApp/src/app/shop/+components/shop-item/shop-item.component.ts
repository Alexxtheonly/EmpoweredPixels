import { ShopItemPriceComponent } from './../shop-item-price/shop-item-price.component';
import { CurrencyBalance } from './../../../inventory/+models/currency-balance';
import { ShopItem } from './../../+models/shop-item';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrls: ['./shop-item.component.css']
})
export class ShopItemComponent implements OnInit
{

  @Input()
  public item: ShopItem;

  @Output()
  public itemBought = new EventEmitter<ShopItem>();

  @Input()
  public balances: CurrencyBalance[];

  @ViewChild('prices', { static: false })
  prices: ShopItemPriceComponent;

  constructor() { }

  ngOnInit()
  {
  }

  public buyItem()
  {
    if (!this.item)
    {
      return;
    }

    this.itemBought.emit(this.item);
  }
}
