import { Item } from './../../rewards/+models/item';
import { Equipment } from './../../roster/+models/equipment';
import { Fighter } from './../../roster/+models/fighter';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-fighter-gear',
  templateUrl: './fighter-gear.component.html',
  styleUrls: ['./fighter-gear.component.css']
})
export class FighterGearComponent implements OnInit
{
  public fighter: Fighter;

  public head: Equipment;
  public shoulders: Equipment;
  public chest: Equipment;
  public hands: Equipment;
  public legs: Equipment;
  public shoes: Equipment;
  public weapon: Equipment;

  @Output()
  fighterChanged = new EventEmitter<Fighter>();

  @Output()
  salvaged = new EventEmitter<Item[]>();

  @Input()
  public disableContext: boolean;

  @Input('fighter')
  set setFighter(fighter: Fighter)
  {
    this.updateFighter(fighter);
  }

  constructor() { }

  ngOnInit()
  {
  }

  public updateFighter(fighter: Fighter): void
  {
    if (!fighter)
    {
      return;
    }

    this.fighter = fighter;
    this.head = fighter.equipment.find(o => o.type === '4408ebb1-9213-45c2-9e6d-8ad22ead4edc');
    this.shoulders = fighter.equipment.find(o => o.type === '47b22820-c3d7-495b-aac8-f29501623723');
    this.chest = fighter.equipment.find(o => o.type === '71e105f2-a098-47d7-a2de-561ca434ec54');
    this.hands = fighter.equipment.find(o => o.type === '640d7e83-9d98-471e-928f-5a4edaa7f4b0');
    this.legs = fighter.equipment.find(o => o.type === '922b5587-94e0-4fd1-a896-8e3f64c71304');
    this.shoes = fighter.equipment.find(o => o.type === '724bc2a7-712a-45c5-8c01-bb9c5582e33b');
    this.weapon = fighter.equipment.find(o => o.isWeapon);

    this.fighterChanged.emit(fighter);
  }

  public updateSalvage(items: Item[]): void
  {
    this.salvaged.emit(items);
  }
}
