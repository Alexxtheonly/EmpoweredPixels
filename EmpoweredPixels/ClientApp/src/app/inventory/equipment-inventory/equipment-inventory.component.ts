import { TranslateService } from '@ngx-translate/core';
import { Page } from './../../match/+models/page';
import { Item } from './../../rewards/+models/item';
import { EquipmentService } from './../../+services/equipment.service';
import { Equipment } from './../../roster/+models/equipment';
import { EquipmentFilter } from './../../+models/equipment-filter';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Fighter } from 'src/app/roster/+models/fighter';
import { PagingOptions } from 'src/app/match/+models/paging-options';

@Component({
  selector: 'app-equipment-inventory',
  templateUrl: './equipment-inventory.component.html',
  styleUrls: ['./equipment-inventory.component.css']
})
export class EquipmentInventoryComponent implements OnInit
{
  public options: PagingOptions = new PagingOptions();

  public filter: EquipmentFilter = new EquipmentFilter();

  public page: Page<Equipment>;

  public loading: boolean;

  public salvageInProgress: boolean;

  @Input()
  public fighter: Fighter;

  @Output()
  public fighterChanged = new EventEmitter<Fighter>();

  @Output()
  salvaged = new EventEmitter<Item[]>();

  constructor(private equipmentService: EquipmentService, private translateService: TranslateService) { }

  ngOnInit()
  {
  }

  public loadEquipment(): void
  {
    this.equipmentService.getInventoryPage(this.options).subscribe(result =>
    {
      this.page = result;
    });
  }

  public salvageInventory(): void
  {
    if (!confirm(this.translateService.instant('inventory.salvageInventoryWarning')))
    {
      return;
    }
    this.salvageInProgress = true;
    this.equipmentService.salvageInventory().subscribe(result =>
    {
      this.updateSalvage(result);
      this.loadEquipment();
      this.salvageInProgress = false;
    }, error =>
    {
      this.salvageInProgress = false;
    });
  }

  public fighterChange(fighter: Fighter): void
  {
    this.loadEquipment();
    this.fighterChanged.emit(fighter);
  }

  public updateSalvage(items: Item[]): void
  {
    this.salvaged.emit(items);
  }

  public loadPage(page: number)
  {
    this.loading = true;
    this.options.pageNumber = page;
    this.loadEquipment();
  }
}
