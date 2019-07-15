import { FighterName } from './+models/fighter-name';
import { ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { RosterService } from './../roster/+services/roster.service';
import { observable, Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatchService } from './../match/+services/match.service';
import { Component, OnInit } from '@angular/core';
import { MatchResult } from './+models/match-result';

@Component({
  selector: 'app-match-viewer',
  templateUrl: './match-viewer.component.html',
  styleUrls: ['./match-viewer.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MatchViewerComponent implements OnInit {

  public match: MatchResult;

  constructor(
    private changeDetectionRef: ChangeDetectorRef,
    private matchService: MatchService,
    private route: ActivatedRoute,
    private rosterService: RosterService) {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.matchService.getMatchResult(id).subscribe(result => {
      this.match = result;
      this.changeDetectionRef.markForCheck();
    });
  }

  ngOnInit() {
  }

  public getFighterName(id: string): Observable<FighterName> {
    return this.rosterService.getFighterName(id);
  }
}
