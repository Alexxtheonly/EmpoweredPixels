import { RosterService } from './../+services/roster.service';
import { Fighter } from './../+models/fighter';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-fighter',
  templateUrl: './fighter.component.html',
  styleUrls: ['./fighter.component.css']
})
export class FighterComponent implements OnInit {
  public fighter: Fighter;
  public powerlevel: number;
  constructor(private route: ActivatedRoute, private rosterService: RosterService) { }

  ngOnInit() {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.rosterService.getFighter(id).subscribe(result => {
      this.fighter = result;
      this.powerlevel = this.getPowerlevel();
    }, error => console.error(error));
  }

  public getPowerlevel(): number {
    return this.fighter.agility
      + this.fighter.expertise
      + this.fighter.power
      + this.fighter.regeneration
      + this.fighter.speed
      + this.fighter.stamina
      + this.fighter.toughness
      + this.fighter.vision
      + this.fighter.vitality;
  }

  public updateFighter(): void {
    this.rosterService.updateFighter(this.fighter).subscribe(result => {
      this.fighter = result;
      this.powerlevel = this.getPowerlevel();
    }, error => console.error(error));
  }

}
