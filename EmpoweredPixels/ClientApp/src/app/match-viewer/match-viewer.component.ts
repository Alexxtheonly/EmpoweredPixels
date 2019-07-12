import { MatchScore } from './+models/match-score';
import { MatchViewerService } from './+services/match-viewer.service';
import { Component, OnInit } from '@angular/core';
import { Match } from './+models/match';

@Component({
  selector: 'app-match-viewer',
  templateUrl: './match-viewer.component.html',
  styleUrls: ['./match-viewer.component.css']
})
export class MatchViewerComponent implements OnInit {

  public match: Match;
  public matchScores: MatchScore[];

  private service: MatchViewerService;


  constructor(service: MatchViewerService) {
    this.service = service;
    this.service.getTestmatch().subscribe((result) => {
      this.match = result;
      this.matchScores = result.scores;
    }, error => console.error(error));

  }

  ngOnInit() {
  }

}
