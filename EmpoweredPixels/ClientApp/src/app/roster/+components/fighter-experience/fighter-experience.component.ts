import { FighterExperience } from './../../../player/+models/fighter-experience';
import { RosterService } from 'src/app/roster/+services/roster.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-fighter-experience',
  templateUrl: './fighter-experience.component.html',
  styleUrls: ['./fighter-experience.component.css']
})
export class FighterExperienceComponent implements OnInit
{
  private id: string;

  public experience: FighterExperience;

  public percent: number;

  @Input()
  set fighterId(fighterId: string)
  {
    this.id = fighterId;
    this.rosterService.getExperience(this.id).subscribe(result =>
    {
      this.experience = result;
      this.percent = (this.experience.currentExp / this.experience.levelExp) * 100;
    });
  }

  constructor(private rosterService: RosterService) { }

  ngOnInit()
  {

  }

}
