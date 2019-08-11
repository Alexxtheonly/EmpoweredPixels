import { LeagueService } from './../+services/league.service';
import { Observable } from 'rxjs';
import { League } from './../+models/league';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-league-browser',
  templateUrl: './league-browser.component.html',
  styleUrls: ['./league-browser.component.css']
})
export class LeagueBrowserComponent implements OnInit
{
  public leagues: Observable<League[]>;

  constructor(private leagueService: LeagueService)
  {
    this.leagues = this.leagueService.getLeagues();
  }

  ngOnInit()
  {
  }

}
