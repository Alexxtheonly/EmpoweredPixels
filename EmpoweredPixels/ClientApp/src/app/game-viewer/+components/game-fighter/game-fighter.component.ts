import { GameFighter } from './../../+models/game-fighter';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-game-fighter',
  templateUrl: './game-fighter.component.html',
  styleUrls: ['./game-fighter.component.css']
})
export class GameFighterComponent implements OnInit
{
  @Input()
  public fighter: GameFighter;


  constructor() { }

  ngOnInit()
  {
  }

}
