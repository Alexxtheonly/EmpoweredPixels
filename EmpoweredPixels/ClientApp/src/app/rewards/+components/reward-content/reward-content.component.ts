import { Equipment } from './../../../roster/+models/equipment';
import { RewardContent } from './../../+models/reward-content';
import { Item } from './../../+models/item';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-reward-content',
  templateUrl: './reward-content.component.html',
  styleUrls: ['./reward-content.component.css']
})
export class RewardContentComponent implements OnInit
{

  @Input()
  public items: Item[];

  @Input()
  public equipment: Equipment[];

  constructor() { }

  ngOnInit()
  {
  }

}
