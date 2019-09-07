import { UserFeedbackService } from './../../+services/userfeedback.service';
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
  public loading: boolean;
  public page: Page<Match>;

  constructor(private matchService: MatchService, matchHubService: MatchHubService, private userfeedbackService: UserFeedbackService)
  {
    matchHubService.connect().then(() =>
    {
      matchHubService.matchCreated$.subscribe(() =>
      {
        if (this.options.pageNumber !== 1)
        {
          return;
        }

        this.loadMatches();
      });
    });
  }

  ngOnInit()
  {
    this.loadMatches();
  }

  public loadPage(page: number)
  {
    this.loading = true;
    this.options.pageNumber = page;
    this.loadMatches();
  }

  private loadMatches(): void
  {
    this.matchService.browse(this.options).subscribe(result =>
    {
      this.page = result;
      this.loading = false;
    }, error =>
    {
      this.userfeedbackService.error(error);
    });
  }

}
