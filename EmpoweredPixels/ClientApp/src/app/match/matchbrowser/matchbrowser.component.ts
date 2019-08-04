import { MatchHubService } from './../+services/match-hub.service';
import { MatchService } from './../+services/match.service';
import { PagingOptions } from './../+models/paging-options';
import { Component, OnInit } from '@angular/core';
import { Match } from '../+models/match';
import { Page } from '../+models/page';

@Component({
  selector: 'app-matchbrowser',
  templateUrl: './matchbrowser.component.html',
  styleUrls: ['./matchbrowser.component.css']
})
export class MatchbrowserComponent implements OnInit
{
  public options: PagingOptions = new PagingOptions();
  public page: Page<Match>;

  constructor(private matchService: MatchService, private matchHubService: MatchHubService)
  {
    matchHubService.connect().then();
  }

  ngOnInit()
  {
    this.loadMatches();
  }

  private loadMatches(): void
  {
    this.matchService.browse(this.options).subscribe(result =>
    {
      this.page = result;
    });
  }

}
