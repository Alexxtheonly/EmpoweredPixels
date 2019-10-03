import { LeagueSubscription } from './../+models/league-subscription';
import { UserFeedbackService } from './../../+services/userfeedback.service';
import { LeagueService } from './../+services/league.service';
import { Observable, of } from 'rxjs';
import { League } from './../+models/league';
import { Component, OnInit } from '@angular/core';
import { catchError } from 'rxjs/operators';
import * as moment from 'moment';
import { LeagueMatchWinner } from '../+models/league-match-winner';

@Component({
  selector: 'app-league-browser',
  templateUrl: './league-browser.component.html',
  styleUrls: ['./league-browser.component.css']
})
export class LeagueBrowserComponent implements OnInit
{
  public leagues: Observable<League[]>;

  constructor(private leagueService: LeagueService, private userfeedbackService: UserFeedbackService)
  {
    this.leagues = this.leagueService.getLeagues().pipe(catchError((error) =>
    {
      this.userfeedbackService.error(error);
      return of(new Array());
    }));
  }

  ngOnInit()
  {
  }

  public getDiff(date: Date): string
  {
    return moment(date).fromNow();
  }
}
