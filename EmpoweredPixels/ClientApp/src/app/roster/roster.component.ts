import { UserFeedbackService } from './../+services/userfeedback.service';
import { Fighter } from './+models/fighter';
import { Component, OnInit } from '@angular/core';
import { RosterService } from './+services/roster.service';
import { observable, Observable } from 'rxjs';

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

  public deleteFighter(fighter: Fighter)
  {
    if (confirm(`Are you sure you want to delete ${fighter.name}?`))
    {
      this.rosterService.deleteFighter(fighter.id).subscribe(result =>
      {
        this.userfeedbackService.success(`Fighter successfully deleted. Farewell ${fighter.name} you shall be missed.`);
        this.loadFighters();
      }, error =>
      {
        this.userfeedbackService.error(error);
      });
    }
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
