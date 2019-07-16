import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-fighter-stat',
  templateUrl: './fighter-stat.component.html',
  styleUrls: ['./fighter-stat.component.css']
})
export class FighterStatComponent implements OnInit {
  @Input()
  public value: number;

  @Output()
  public valueChange: EventEmitter<number> = new EventEmitter();

  @Input()
  public name: string;

  @Input()
  public description: string;

  @Input()
  public powerlevel: number;

  @Output()
  public powerlevelChange: EventEmitter<number> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  public increase(value: number) {
    this.value += value;
    this.powerlevel += value;

    this.emitChanges();
  }

  public decrease(value: number) {
    if ((this.value - value) < 1) {
      return;
    }

    this.value -= value;
    this.powerlevel -= value;

    this.emitChanges();
  }

  private emitChanges(): void {
    this.valueChange.emit(this.value);
    this.powerlevelChange.emit(this.powerlevel);
  }
}
