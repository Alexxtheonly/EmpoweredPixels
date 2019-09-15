import { UserFeedbackService } from './../../+services/userfeedback.service';
import { RosterService } from './../+services/roster.service';
import { Fighter } from './../+models/fighter';
import { Component, OnInit, Output, EventEmitter, OnChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-fighter',
  templateUrl: './fighter.component.html',
  styleUrls: ['./fighter.component.css']
})
export class FighterComponent implements OnInit
{
  public fighter: Fighter;

  constructor(
    private route: ActivatedRoute,
    private rosterService: RosterService,
    private userfeedbackService: UserFeedbackService,
    private router: Router) { }

  ngOnInit()
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.rosterService.getFighter(id).subscribe(result =>
    {
      this.fighter = result;
    }, error => this.userfeedbackService.error(error));
  }

  public updateFighter(): void
  {
    this.rosterService.updateFighter(this.fighter).subscribe(result =>
    {
      this.fighter = result;
      this.userfeedbackService.success('Fighter attributes successfully saved');
    }, error =>
    {
      console.error(error);
      this.userfeedbackService.error(error);
    });
  }

  public resetFighter(): void
  {
    this.fighter.agility = 1;
    this.fighter.expertise = 1;
    this.fighter.power = 1;
    this.fighter.regeneration = 1;
    this.fighter.speed = 1;
    this.fighter.stamina = 1;
    this.fighter.toughness = 1;
    this.fighter.vision = 1;
    this.fighter.vitality = 1;
    this.fighter.accuracy = 1;
  }

  public deleteFighter(fighter: Fighter)
  {
    if (confirm(`Are you sure you want to delete ${fighter.name}?`))
    {
      this.rosterService.deleteFighter(fighter.id).subscribe(result =>
      {
        this.userfeedbackService.success(`Fighter successfully deleted. Farewell ${fighter.name} you shall be missed.`);
        this.router.navigate(['/roster']);
      }, error =>
      {
        this.userfeedbackService.error(error);
      });
    }
  }

}