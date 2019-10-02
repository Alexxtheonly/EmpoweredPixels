import { Item } from './../../rewards/+models/item';
import { EquipmentInventoryComponent } from './../../inventory/equipment-inventory/equipment-inventory.component';
import { TranslateService } from '@ngx-translate/core';
import { UserFeedbackService } from './../../+services/userfeedback.service';
import { RosterService } from './../+services/roster.service';
import { Fighter } from './../+models/fighter';
import { Component, OnInit, Output, EventEmitter, OnChanges, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-fighter',
  templateUrl: './fighter.component.html',
  styleUrls: ['./fighter.component.css']
})
export class FighterComponent implements OnInit
{
  public fighter: Fighter;

  public showSalvage: boolean;

  public items: Item[];

  @ViewChild('inventory', { static: false })
  inventory: EquipmentInventoryComponent;

  constructor(
    private route: ActivatedRoute,
    private rosterService: RosterService,
    private userfeedbackService: UserFeedbackService,
    private router: Router,
    private translateService: TranslateService) { }

  ngOnInit()
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.rosterService.getFighter(id).subscribe(result =>
    {
      this.fighter = result;
    }, error => this.userfeedbackService.error(error));
  }

  public deleteFighter(fighter: Fighter)
  {
    if (confirm(this.translateService.instant('roster.fighterDeleteConfirm', { name: fighter.name })))
    {
      this.rosterService.deleteFighter(fighter.id).subscribe(result =>
      {
        this.userfeedbackService.success(this.translateService.instant('roster.fighterDeleted', { name: fighter.name }));
        this.router.navigate(['/roster']);
      }, error =>
      {
        this.userfeedbackService.error(error);
      });
    }
  }

  public updateFighter(fighter: Fighter): void
  {
    this.fighter = fighter;
    this.inventory.loadEquipment();
  }

  public updateSalvage(items: Item[]): void
  {
    this.items = items;
    if (items.length > 0)
    {
      this.showSalvage = true;
    }
  }
}
