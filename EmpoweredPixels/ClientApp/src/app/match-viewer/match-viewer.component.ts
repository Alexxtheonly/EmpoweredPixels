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
export class MatchViewerComponent implements OnInit
{

  public match$: Observable<MatchResult>;

  public id: string;

  constructor(
    private matchService: MatchService,
    private route: ActivatedRoute)
  {
  }

  ngOnInit()
  {
    this.id = this.route.snapshot.paramMap.get('id');
    this.match$ = this.matchService.getMatchResult(this.id);
  }

  public getTeamColor(teamId: string): string
  {
    if (!teamId)
    {
      return;
    }

    return '#' + teamId.substring(0, 6);
  }
}
