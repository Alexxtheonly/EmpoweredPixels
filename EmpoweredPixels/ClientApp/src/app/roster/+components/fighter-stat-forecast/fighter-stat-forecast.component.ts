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
  }
}
