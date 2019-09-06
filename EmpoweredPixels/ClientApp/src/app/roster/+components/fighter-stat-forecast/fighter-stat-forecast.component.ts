import { RosterService } from './../../+services/roster.service';
import { FighterStatForecast } from './../../+models/fighter-stat-forecast';
import { Component, OnInit, Input, OnChanges, DoCheck } from '@angular/core';
import { Fighter } from '../../+models/fighter';

@Component({
  selector: 'app-fighter-stat-forecast',
  templateUrl: './fighter-stat-forecast.component.html',
  styleUrls: ['./fighter-stat-forecast.component.css']
})
export class FighterStatForecastComponent implements OnInit, DoCheck
{

  public forecast: FighterStatForecast;

  @Input()
  public fighter: Fighter;

  private previous: Fighter;

  constructor(private rosterService: RosterService)
  {
  }

  ngOnInit()
  {
  }

  ngDoCheck()
  {
    if (!this.fighter)
    {
      return;
    }

    const changed = this.hasChanged(this.previous, this.fighter);

    this.previous = Object.assign({}, this.fighter);

    if (!changed)
    {
      return;
    }

    this.rosterService.getFighterStatForecast(this.fighter).subscribe(forecast =>
    {
      this.forecast = forecast;
    });
  }

  private hasChanged(left: Fighter, right: Fighter): boolean
  {
    if (!left || !right)
    {
      return true;
    }

    return left.agility !== right.agility ||
      left.expertise !== right.expertise ||
      left.power !== right.power ||
      left.regeneration !== right.regeneration ||
      left.speed !== right.speed ||
      left.stamina !== right.stamina ||
      left.toughness !== right.toughness ||
      left.vision !== right.vision ||
      left.vitality !== right.vitality ||
      left.accuracy !== right.accuracy;
  }
}
