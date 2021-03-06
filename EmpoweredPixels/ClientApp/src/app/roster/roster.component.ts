import { UserFeedbackService } from './../+services/userfeedback.service';
import { Fighter } from './+models/fighter';
import { Component, OnInit } from '@angular/core';
import { RosterService } from './+services/roster.service';

@Component({
  selector: 'app-roster',
  templateUrl: './roster.component.html',
  styleUrls: ['./roster.component.css']
})
export class RosterComponent implements OnInit
{
  public fighters: Fighter[] = new Array();

  constructor(private rosterService: RosterService, private userfeedbackService: UserFeedbackService) { }

  ngOnInit()
  {
    this.loadFighters();
  }

  private loadFighters()
  {
    this.rosterService.getFighters().subscribe(result =>
    {
      this.fighters = result;
    }, error =>
    {
      this.userfeedbackService.error(error);
    });
  }
}
