import { Item } from './../../+models/item';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-reward-content',
  templateUrl: './reward-content.component.html',
  styleUrls: ['./reward-content.component.css']
})
export class RewardContentComponent implements OnInit
{

  @Input()
  public items: Item[];

  constructor() { }

  ngOnInit()
  {
  }

}
