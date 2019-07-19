import { Observable } from 'rxjs';
import { RosterService } from './../../roster/+services/roster.service';
import { Fighter } from './../../roster/+models/fighter';
import { MatchRegistration } from './../+models/match-registration';
import { MatchService } from './../+services/match.service';
import { Match } from './../+models/match';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-matchlobby',
  templateUrl: './matchlobby.component.html',
  styleUrls: ['./matchlobby.component.css']
})
export class MatchlobbyComponent implements OnInit {
  private match: Match;
  private fighters: Observable<Fighter[]>;
  private fighterId: string;

  constructor(
    private matchService: MatchService,
    private route: ActivatedRoute,
    private rosterService: RosterService,
    private router: Router) { }

  ngOnInit() {
    this.loadMatch();

    this.fighters = this.rosterService.getFighters();
  }

  private loadMatch() {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.matchService.getMatch(id).subscribe(result => {
      this.match = result;
    }, error => console.error(error));
  }

  public join(): void {
    if (!this.fighterId) {
      return;
    }

    const registration: MatchRegistration = new MatchRegistration();
    registration.matchId = this.match.id;
    registration.fighterId = this.fighterId;

    this.matchService.joinMatch(registration).subscribe(result => {
      this.loadMatch();
    });
  }

  public startMatch(): void {
    this.matchService.startMatch(this.match).subscribe(result => {
      this.router.navigate([`match/${this.match.id}/result`]);
    });
  }
}
