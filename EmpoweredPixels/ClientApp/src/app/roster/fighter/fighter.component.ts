import { AlertService } from 'src/app/+services/alert.service';
import { RosterService } from './../+services/roster.service';
import { Fighter } from './../+models/fighter';
import { Component, OnInit, Output, EventEmitter, OnChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-fighter',
  templateUrl: './fighter.component.html',
  styleUrls: ['./fighter.component.css']
})
export class FighterComponent implements OnInit
{
  public fighter: Fighter;

  public powerlevel: number;
  constructor(private route: ActivatedRoute, private rosterService: RosterService, private alertService: AlertService) { }

  ngOnInit()
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.rosterService.getFighter(id).subscribe(result =>
    {
      this.fighter = result;
      this.powerlevel = this.getPowerlevel();
    }, error => console.error(error));
  }

  public getPowerlevel(): number
  {
    return this.fighter.agility
      + this.fighter.expertise
      + this.fighter.power
      + this.fighter.regeneration
      + this.fighter.speed
      + this.fighter.stamina
      + this.fighter.toughness
      + this.fighter.vision
      + this.fighter.vitality
      + this.fighter.accuracy;
  }

  public updateFighter(): void
  {
    this.rosterService.updateFighter(this.fighter).subscribe(result =>
    {
      this.fighter = result;
      this.powerlevel = this.getPowerlevel();
      this.alertService.success('Fighter attributes successfully saved');
    }, error =>
    {
      console.error(error);
      this.alertService.error('Error while saving');
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

    this.powerlevel = this.getPowerlevel();
  }

}