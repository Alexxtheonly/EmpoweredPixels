import { observable, Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatchService } from './../match/+services/match.service';
import { MatchScore } from './+models/match-score';
import { MatchViewerService } from './+services/match-viewer.service';
import { Component, OnInit } from '@angular/core';
import { MatchResult } from './+models/match-result';

@Component({
  selector: 'app-match-viewer',
  templateUrl: './match-viewer.component.html',
  styleUrls: ['./match-viewer.component.css']
})
export class MatchViewerComponent implements OnInit {

  public match: Observable<MatchResult>;

  constructor(private matchService: MatchService, private route: ActivatedRoute) {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.match = this.matchService.getMatchResult(id);
  }

  ngOnInit() {
  }

}
