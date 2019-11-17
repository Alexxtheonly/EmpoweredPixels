import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-fighter-attunements',
  templateUrl: './fighter-attunements.component.html',
  styleUrls: ['./fighter-attunements.component.css']
})
export class FighterAttunementsComponent implements OnInit
{

  @Input()
  public selected: string;

  @Output()
  public selectedChange = new EventEmitter<string>();

  constructor() { }

  ngOnInit()
  {
  }

  public selectedChanged(id: string)
  {
    this.selectedChange.emit(id);
  }

}
