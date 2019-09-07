import { UserFeedbackService } from './../../+services/userfeedback.service';
import { Component, OnInit } from '@angular/core';
import { RosterService } from '../+services/roster.service';
import { Router } from '@angular/router';
import { Fighter } from '../+models/fighter';

@Component({
  selector: 'app-new-fighter',
  templateUrl: './new-fighter.component.html',
  styleUrls: ['./new-fighter.component.css']
})
export class NewFighterComponent implements OnInit
{
  public name: string;
  public loading: boolean;

  constructor(private rosterService: RosterService, private router: Router, private userfeedbackService: UserFeedbackService) { }

  ngOnInit()
  {
  }

  public submit(): void
  {
    if (name || this.name.length === 0)
    {
      return;
    }

    this.loading = true;

    const fighter = new Fighter();
    fighter.name = this.name;

    this.rosterService.createFighter(fighter).subscribe(result =>
    {
      this.router.navigate([`roster/fighter/${result.id}`]);
    }, error => this.userfeedbackService.error(error));
  }
}
