import { LeagueSubscription } from './../+models/league-subscription';
import { UserFeedbackService } from './../../+services/userfeedback.service';
import { LeagueHighscoreOptions } from './../+models/league-highscore-options';
import { RosterService } from './../../roster/+services/roster.service';
import { LeagueMatch } from './../+models/league-match';
import { LeagueService } from './../+services/league.service';
import { Observable } from 'rxjs';
import { LeagueDetail } from './../+models/league-detail';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Fighter } from 'src/app/roster/+models/fighter';
import { PagingOptions } from 'src/app/match/+models/paging-options';
import { Match } from 'src/app/match/+models/match';
import { Page } from 'src/app/match/+models/page';
import { LeagueHighscore } from '../+models/league-highscore';

@Component({
  selector: 'app-league-detail',
  templateUrl: './league-detail.component.html',
  styleUrls: ['./league-detail.component.css']
})
export class LeagueDetailComponent implements OnInit
{
  public leagueDetail: LeagueDetail;
  public fighters: Fighter[];

  public options: PagingOptions = new PagingOptions();
  public loading: boolean;
  public page: Page<LeagueMatch>;

  public showModal: boolean;

  public fighterId: string = null;
  public subs: LeagueSubscription[];

  public highscores: LeagueHighscore[];
  public leagueHighscoreOptions: LeagueHighscoreOptions = new LeagueHighscoreOptions();

  private id: number;

  constructor(
    private leagueService: LeagueService,
    private route: ActivatedRoute,
    private rosterService: RosterService,
    private userfeedbackService: UserFeedbackService)
  {
    this.options.pageSize = 5;

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadLeague();
    this.loadLeagueMatches();

    rosterService.getFighters().subscribe(result =>
    {
      this.fighters = result;
    });

    this.loadUserSubs();

    this.leagueHighscoreOptions.lastMatches = 25;
    this.leagueHighscoreOptions.top = 5;
    this.loadLeagueHighscores();
  }

  private loadUserSubs()
  {
    this.leagueService.getUserSubscriptions(this.id).subscribe(result =>
    {
      this.subs = result;
    });
  }

  ngOnInit()
  {
  }

  public subscribe(): void
  {
    if (!this.fighterId && this.fighters.length > 1)
    {
      this.showModal = true;
      return;
    } else
    {
      this.fighterId = this.fighters[0].id;
    }

    this.leagueService.subscribeLeague(this.id, this.fighterId).subscribe(result =>
    {
      this.loadLeague();
      this.loadUserSubs();
    }, error =>
    {
      this.userfeedbackService.error(error);
    });

    this.fighterId = null;
  }

  public unsubscribe(): void
  {
    if (!this.fighterId && this.subs.length > 1)
    {
      this.showModal = true;
      return;
    } else
    {
      this.fighterId = this.subs[0].fighterId;
    }

    this.leagueService.unsubscribeLeague(this.id, this.fighterId).subscribe(result =>
    {
      this.loadLeague();
      this.loadUserSubs();
    }, error =>
    {
      this.userfeedbackService.error(error);
    });

    this.fighterId = null;
  }

  private loadLeague(): void
  {
    this.leagueService.getLeague(this.id).subscribe(result =>
    {
      this.leagueDetail = result;
      this.loading = false;
    }, error =>
    {
      this.userfeedbackService.error(error);
    });
  }

  private loadLeagueMatches()
  {
    this.leagueService.getLeagueMatches(this.id, this.options).subscribe(result =>
    {
      this.page = result;
      this.loading = false;
    }, error =>
    {
      this.userfeedbackService.error(error);
    });
  }

  private loadLeagueHighscores()
  {
    this.leagueService.getLeagueHighscores(this.id, this.leagueHighscoreOptions).subscribe(result =>
    {
      this.highscores = result;
    }, error =>
    {
      this.userfeedbackService.error(error);
    });
  }

  public loadPage(page: number)
  {
    this.loading = true;
    this.options.pageNumber = page;
    this.loadLeagueMatches();
  }
}
