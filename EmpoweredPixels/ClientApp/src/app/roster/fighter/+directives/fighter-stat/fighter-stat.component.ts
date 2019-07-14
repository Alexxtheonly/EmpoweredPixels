import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-fighter-stat',
  templateUrl: './fighter-stat.component.html',
  styleUrls: ['./fighter-stat.component.css']
})
export class FighterStatComponent implements OnInit {
  @Input()
  public value: number;

  @Input()
  public name: string;

  @Input()
  public description: string;

  @Input()
  public powerlevel: number;

  constructor() { }

  ngOnInit() {
  }

  public increase() {
    this.value++;
    this.powerlevel++;
  }

  public decrease() {
    if (this.value <= 1) {
      return;
    }

    this.value--;
    this.powerlevel--;
  }

}
