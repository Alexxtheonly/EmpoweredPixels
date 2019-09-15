import { RosterService } from './../../+services/roster.service';
import { FighterStatForecast } from './../../+models/fighter-stat-forecast';
import { Component, OnInit, Input, OnChanges, DoCheck } from '@angular/core';
import { Fighter } from '../../+models/fighter';
import { UserFeedbackService } from '../../../+services/userfeedback.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fighter-stat-forecast',
  templateUrl: './fighter-stat-forecast.component.html',
  styleUrls: ['./fighter-stat-forecast.component.css']
})
export class FighterStatForecastComponent implements OnInit, DoCheck
{

  public forecast: FighterStatForecast;
  public powerlevel = 0;

  @Input()
  public fighter: Fighter;

  private previous: Fighter;

  constructor(
    private router: Router,
    private userfeedbackService: UserFeedbackService,
    private rosterService: RosterService)
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

    this.powerlevel = this.getPowerlevel();

    this.rosterService.getFighterStatForecast(this.fighter).subscribe(forecast =>
    {
      this.forecast = forecast;
    });
  }

  public deleteFighter()
  {
    if (confirm(`Are you sure you want to delete ${this.fighter.name}?`))
    {
      this.rosterService.deleteFighter(this.fighter.id).subscribe(result =>
      {
        this.userfeedbackService.success(`Fighter successfully deleted. Farewell ${this.fighter.name} you shall be missed.`);
        this.router.navigate(['roster']);
      }, error =>
      {
        this.userfeedbackService.error(error);
      });
    }
  }

  public getPowerlevel(): number
  {
    return this.fighter.agility
      + this.fighter.expertise
      + this.fighter.power
      + this.fighter.regeneration
      + this.fighter.speed
      + this.fighter.stamina
      + this.fighter.toughness
      + this.fighter.vision
      + this.fighter.vitality
      + this.fighter.accuracy;
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
