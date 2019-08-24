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

}
