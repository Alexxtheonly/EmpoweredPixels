import { MatchHubService } from './../+services/match-hub.service';
import { Observable } from 'rxjs';
import { RosterService } from './../../roster/+services/roster.service';
import { Fighter } from './../../roster/+models/fighter';
import { MatchRegistration } from './../+models/match-registration';
import { MatchService } from './../+services/match.service';
import { Match } from './../+models/match';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-matchlobby',
  templateUrl: './matchlobby.component.html',
  styleUrls: ['./matchlobby.component.css']
})
export class MatchlobbyComponent implements OnInit, OnDestroy {
  public match: Match;
  public fighters: Observable<Fighter[]>;
  public fighterId: string;

  constructor(
    private matchService: MatchService,
    private route: ActivatedRoute,
    private rosterService: RosterService,
    private router: Router,
    private matchHubService: MatchHubService) {
    matchHubService.connect().then(() => {
      matchHubService.matchUpdated$.subscribe((match: Match) => {
        this.updateMatch(match);
      });
    });
  }

  ngOnInit() {
    this.loadMatch();

    this.fighters = this.rosterService.getFighters();
  }

  ngOnDestroy(): void {
    this.matchHubService.leaveGroup(this.match);
    this.matchHubService.disconnect();
  }

  private loadMatch() {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.matchService.getMatch(id).subscribe(result => {
      this.match = result;

      if (result.ended) {
        this.navigateToResult();
      } else {
        this.matchHubService.joinGroup(this.match);
      }
    }, error => console.error(error));
  }

  public join(): void {
    if (!this.fighterId) {
      return;
    }

    const registration: MatchRegistration = new MatchRegistration();
    registration.matchId = this.match.id;
    registration.fighterId = this.fighterId;

    this.matchService.joinMatch(registration).subscribe();
  }

  public leave(): void {
    this.fighters.subscribe((fighters) => {
      for (const fighter of fighters) {
        const registration = this.match.registrations.find(o => o.fighterId === fighter.id);
        if (registration) {
          this.matchService.leaveMatch(registration).subscribe();
        }
      }
    });
  }

  public startMatch(): void {
    this.matchService.startMatch(this.match).subscribe(result => {
      this.navigateToResult();
    });
  }

  private navigateToResult() {
    this.router.navigate([`match/${this.match.id}/result`]);
  }

  private updateMatch(match: Match): void {
    if (match.ended) {
      this.navigateToResult();
    } else {
      this.match = match;
    }
  }
}
