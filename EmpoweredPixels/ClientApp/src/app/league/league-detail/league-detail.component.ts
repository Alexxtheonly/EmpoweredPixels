import { RosterService } from './../../roster/+services/roster.service';
import { LeagueMatch } from './../+models/league-match';
import { LeagueService } from './../+services/league.service';
import { Observable } from 'rxjs';
import { LeagueDetail } from './../+models/league-detail';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Fighter } from 'src/app/roster/+models/fighter';

@Component({
  selector: 'app-league-detail',
  templateUrl: './league-detail.component.html',
  styleUrls: ['./league-detail.component.css']
})
export class LeagueDetailComponent implements OnInit
{
  public leagueDetail: LeagueDetail;
  public matches: LeagueMatch[];
  public fighters: Observable<Fighter[]>;

  public fighterId: string;

  private id: number;

  constructor(private leagueService: LeagueService, private route: ActivatedRoute, private rosterService: RosterService)
  {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadLeague();
    leagueService.getLeagueMatches(this.id).subscribe(result =>
    {
      this.matches = result;
    });
    this.fighters = rosterService.getFighters();
  }

  ngOnInit()
  {
  }

  public subscribe(): void
  {
    if (!this.fighterId)
    {
      return;
    }

    this.leagueService.subscribeLeague(this.id, this.fighterId).subscribe(result =>
    {
      this.loadLeague();
    });
  }

  public unsubscribe(): void
  {
    if (!this.fighterId)
    {
      return;
    }

    this.leagueService.unsubscribeLeague(this.id, this.fighterId).subscribe(result =>
    {
      this.loadLeague();
    });
  }

  private loadLeague(): void
  {
    this.leagueService.getLeague(this.id).subscribe(result =>
    {
      this.leagueDetail = result;
    });
  }
}
