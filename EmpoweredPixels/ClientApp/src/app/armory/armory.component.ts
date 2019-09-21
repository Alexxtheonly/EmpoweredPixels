import { FighterArmory } from './+models/fighter-armory';
import { Observable } from 'rxjs';
import { ArmoryService } from './+services/armory.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-armory',
  templateUrl: './armory.component.html',
  styleUrls: ['./armory.component.css']
})
export class ArmoryComponent implements OnInit
{
  public fighterArmory: FighterArmory;

  constructor(private route: ActivatedRoute, private armoryService: ArmoryService) { }

  ngOnInit()
  {
    const fighterId = this.route.snapshot.paramMap.get('id');
    this.armoryService.getFighterArmory(fighterId).subscribe(result =>
    {
      this.fighterArmory = result;
    });
  }

}
