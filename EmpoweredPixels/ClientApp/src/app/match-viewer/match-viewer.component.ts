import { observable, Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatchService } from './../match/+services/match.service';
import { Component, OnInit } from '@angular/core';
import { MatchResult } from './+models/match-result';

@Component({
  selector: 'app-match-viewer',
  templateUrl: './match-viewer.component.html',
  styleUrls: ['./match-viewer.component.css']
})
export class MatchViewerComponent implements OnInit {

  public match: MatchResult;

  constructor(private matchService: MatchService, private route: ActivatedRoute) {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.matchService.getMatchResult(id).subscribe(result => {
      this.match = result;
    });
  }

  ngOnInit() {
  }

}
