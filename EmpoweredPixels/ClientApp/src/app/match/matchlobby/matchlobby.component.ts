import { MatchTeamOperation } from './../+models/match-team-operation';
import { MatchTeam } from './../+models/match-team';
import { AuthService } from './../../auth/auth.service';
import { MatchHubService } from './../+services/match-hub.service';
import { Observable } from 'rxjs';
import { RosterService } from './../../roster/+services/roster.service';
import { Fighter } from './../../roster/+models/fighter';
import { MatchRegistration } from './../+models/match-registration';
import { MatchService } from './../+services/match.service';
import { Match } from './../+models/match';
import { Component, OnInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupByPipe, OrderByPipe } from 'angular-pipes';

@Component({
  selector: 'app-matchlobby',
  templateUrl: './matchlobby.component.html',
  styleUrls: ['./matchlobby.component.css']
})
export class MatchlobbyComponent implements OnInit, OnDestroy
{
  public match: Match;
  public fighters: Observable<Fighter[]>;
  public teams: MatchTeam[];
  public fighterId: string;
  public canStartMatch: boolean;

  public password?: string;
  public teamId: string;
  public showTeamModal: boolean;

  constructor(
    private matchService: MatchService,
    private route: ActivatedRoute,
    private rosterService: RosterService,
    private router: Router,
    private matchHubService: MatchHubService,
    private authService: AuthService)
  {
    matchHubService.connect().then(() =>
    {
      matchHubService.matchUpdated$.subscribe((match: Match) =>
      {
        this.updateMatch(match);
      });
    });
  }

  ngOnInit()
  {
    this.loadMatch();

    this.fighters = this.rosterService.getFighters();
  }

  ngOnDestroy(): void
  {
    this.matchHubService.leaveGroup(this.match);
  }

  private loadMatch()
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.matchService.getMatch(id).subscribe(result =>
    {
      this.match = result;

      if (result.ended)
      {
        this.navigateToResult();
      } else
      {
        this.matchHubService.joinGroup(this.match);
        this.canStartMatch = this.match.creatorUserId === this.authService.getUserId();
      }
    }, error => console.error(error));
  }

  public join(): void
  {
    if (!this.fighterId)
    {
      return;
    }

    const registration: MatchRegistration = new MatchRegistration();
    registration.matchId = this.match.id;
    registration.fighterId = this.fighterId;

    this.matchService.joinMatch(registration).subscribe();
  }

  public leave(): void
  {
    this.fighters.subscribe((fighters) =>
    {
      for (const fighter of fighters)
      {
        const registration = this.match.registrations.find(o => o.fighterId === fighter.id);
        if (registration)
        {
          this.matchService.leaveMatch(registration).subscribe();
        }
      }
    });
  }

  public async createAndJoinTeam(): Promise<void>
  {
    const team = await this.matchService.createTeam(this.match, this.password).toPromise();
    await this.matchService.joinTeam(this.match, team.id, this.fighterId, this.password).subscribe();
  }

  public joinTeam(teamId: string): void
  {
    this.matchService.joinTeam(this.match, teamId, this.fighterId, this.password).subscribe();
  }

  public leaveTeam(): void
  {
    this.matchService.leaveTeam(this.match, this.fighterId).subscribe();
  }

  public getTeamColor(id: string): string
  {
    if (!id)
    {
      return;
    }

    return '#' + id.substr(0, 6);
  }

  public startMatch(): void
  {
    this.matchService.startMatch(this.match).subscribe(result =>
    {
      this.navigateToResult();
    });
  }

  private navigateToResult()
  {
    this.router.navigate([`match/${this.match.id}/result`]);
  }

  private updateMatch(match: Match): void
  {
    if (match.ended)
    {
      this.navigateToResult();
    } else
    {
      this.match = match;
    }
  }
}
