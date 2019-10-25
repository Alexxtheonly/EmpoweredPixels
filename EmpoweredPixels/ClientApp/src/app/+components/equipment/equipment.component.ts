import { TranslateService } from '@ngx-translate/core';
import { Fighter } from 'src/app/roster/+models/fighter';
import { RosterService } from './../../roster/+services/roster.service';
import { Item } from './../../rewards/+models/item';
import { EquipmentService } from './../../+services/equipment.service';
import { Equipment } from './../../roster/+models/equipment';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit
{
  public modalVisible: boolean;

  @Input()
  public disableContext: boolean;

  @Input()
  public equipment: Equipment;

  @Input()
  public fighter: Fighter;

  @Output()
  fighterChange = new EventEmitter<Fighter>();

  @Output()
  salvaged = new EventEmitter<Item[]>();

  constructor(
    private equipmentService: EquipmentService,
    private rosterService: RosterService,
    private translateService: TranslateService) { }

  ngOnInit()
  {
  }

  public getCssRarity(): string
  {
    if (!this.equipment)
    {
      return '';
    }

    switch (this.equipment.rarity)
    {
      case 0:
        return 'rarity-basic';
      case 1:
        return 'rarity-common';
      case 2:
        return 'rarity-rare';
      case 3:
        return 'rarity-fabled';
      case 4:
        return 'rarity-mythic';
      case 5:
        return 'rarity-legendary';
    }
  }

  public getLevelColor(): string
  {
    const levelDiff = this.fighter.level - this.equipment.level;

    if (levelDiff < 0)
    {
      return 'itemlevel-strong';
    } else if (levelDiff > 8)
    {
      return 'itemlevel-weak';
    } else
    {
      return '';
    }
  }

  public isLevelTooHigh()
  {
    if (!this.fighter)
    {
      return false;
    }

    return this.fighter.level < this.equipment.level;
  }

  public setFavorite()
  {
    this.equipmentService.setFavorite(this.equipment.id).subscribe(result =>
    {
      this.equipment = result;
      this.hideModal();
    });
  }

  public unsetFavorite()
  {
    this.equipmentService.unsetFavorite(this.equipment.id).subscribe(result =>
    {
      this.equipment = result;
      this.hideModal();
    });
  }

  public getTranslation(): string
  {
    if (!this.equipment)
    {
      return '';
    }

    return `equipment.${this.equipment.type}`;
  }

  public showModal(): void
  {
    if (this.equipment === undefined || this.disableContext)
    {
      return;
    }

    this.modalVisible = true;
  }

  public hideModal(): void
  {
    this.modalVisible = false;
  }

  public salvage(): void
  {
    if (!confirm(this.translateService.instant('salvageConfirm',
      {
        name: this.translateService.instant(this.getTranslation()),
        rarity: this.translateService.instant('rarity.' + this.equipment.rarity)
      })))
    {
      return;
    }

    this.modalVisible = false;
    this.equipmentService.salvage(this.equipment).subscribe(result =>
    {
      this.equipment = undefined;
      this.salvaged.emit(result);

      if (this.fighter)
      {
        this.rosterService.getFighter(this.fighter.id).subscribe(fighter =>
        {
          this.fighterChange.emit(fighter);
        });
      }
    });
  }

  public enhance(): void
  {
    this.modalVisible = false;
    this.equipmentService.enhance(this.equipment).subscribe(result =>
    {
      this.equipment = result;
    });
  }

  public equip(): void
  {
    if (!this.fighter)
    {
      return;
    }

    this.rosterService.equip(this.fighter.id, this.equipment).subscribe(result =>
    {
      this.fighterChange.emit(result);
      this.modalVisible = false;
    });
  }

  public unequip(): void
  {
    if (!this.fighter)
    {
      return;
    }

    this.rosterService.unequip(this.fighter.id, this.equipment).subscribe(result =>
    {
      this.fighterChange.emit(result);
      this.modalVisible = false;
    });
  }

  public isUnequipped(): boolean
  {
    const fighterId: string = this.equipment.fighterId;
    return !fighterId;
  }
}
