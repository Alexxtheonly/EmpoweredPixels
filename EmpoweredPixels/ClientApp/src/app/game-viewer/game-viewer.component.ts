import { FighterSpawnedTick } from './../match-viewer/+models/fighter-spawned-tick';
import { GameFighter } from './+models/game-fighter';
import { RoundTick } from './../match-viewer/+models/round-tick';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-game-viewer',
  templateUrl: './game-viewer.component.html',
  styleUrls: ['./game-viewer.component.css']
})
export class GameViewerComponent implements OnInit
{

  @Input()
  public mapWidth: number;

  @Input()
  public mapHeight: number;

  @Input()
  public roundTicks: RoundTick[];

  public fighters: GameFighter[] = new Array();

  constructor() { }

  ngOnInit()
  {
    const round0 = this.roundTicks.find(o => o.round === 0);
    round0.ticks.forEach(o =>
    {
      const spawnTick = o as FighterSpawnedTick;

      const fighter = new GameFighter();
      fighter.id = spawnTick.fighterId;
      fighter.health = spawnTick.Health;
      fighter.energy = spawnTick.Energy;
      fighter.positionX = spawnTick.positionX;
      fighter.positionY = spawnTick.positionY;

      this.fighters.push(fighter);
    });
  }

  public nextTick()
  {

  }

}
