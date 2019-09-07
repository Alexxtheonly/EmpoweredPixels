import { UserFeedbackService } from './../../+services/userfeedback.service';
import { Observable } from 'rxjs';
import { MatchOptions } from './../+models/match-options';
import { Router } from '@angular/router';
import { MatchService } from './../+services/match.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-match-creation',
  templateUrl: './match-creation.component.html',
  styleUrls: ['./match-creation.component.css']
})
export class MatchCreationComponent implements OnInit
{
  public matchOptions: MatchOptions;
  public sizes: Observable<Array<string>>;

  constructor(private matchService: MatchService, private router: Router, private userfeedbackService: UserFeedbackService) { }

  ngOnInit()
  {
    this.matchService.getDefaultMatchOptions().subscribe(result =>
    {
      this.matchOptions = result;
    });

    this.sizes = this.matchService.getAvailableSizes();
  }

  public createMatch(): void
  {
    this.matchService.createMatch(this.matchOptions).subscribe(result =>
    {
      this.router.navigate([`match/${result.id}`]);
    }, error =>
    {
      this.userfeedbackService.error(error);
    });
  }
}
