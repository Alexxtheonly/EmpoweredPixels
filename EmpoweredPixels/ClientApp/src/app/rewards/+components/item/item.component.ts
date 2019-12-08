import { Item } from './../../+models/item';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit
{

  @Input()
  public item: Item;

  @Input()
  public count: number;

  constructor() { }

  ngOnInit()
  {
  }

  public getCssRarity(): string
  {
    if (!this.item)
    {
      return '';
    }

    switch (this.item.rarity)
    {
      case 0:
        return 'rarity-basic';
      case 1:
        return 'rarity-common';
      case 2:
        return 'rarity-rare';
      case 3:
        return 'rarity-fabled';
      case 4:
        return 'rarity-mythic';
      case 5:
        return 'rarity-legendary';
    }
  }

}
