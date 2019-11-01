import { Observable } from 'rxjs';
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
  public experience$: Observable<FighterExperience>;

  @Input()
  set fighterId(fighterId: string)
  {
    this.experience$ = this.rosterService.getExperience(fighterId);
  }

  constructor(private rosterService: RosterService) { }

  ngOnInit()
  {

  }

  public getPercent(experience: FighterExperience): number
  {
    return (experience.currentExp / experience.levelExp) * 100;
  }
}
