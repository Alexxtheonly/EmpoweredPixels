import { CurrencyBalance } from './../+models/currency-balance';
import { InventoryService } from './../+services/inventory.service';
import { Equipment } from './../../roster/+models/equipment';
import { EquipmentService } from './../../+services/equipment.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-enhancement',
  templateUrl: './enhancement.component.html',
  styleUrls: ['./enhancement.component.css']
})
export class EnhancementComponent implements OnInit
{
  public enhancementInProgress: boolean;

  public equipment: Equipment;

  public costs: number;

  public balance: CurrencyBalance;

  constructor(private route: ActivatedRoute, private equipmentService: EquipmentService, private inventoryService: InventoryService)
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    equipmentService.getEquipment(id).subscribe(result =>
    {
      this.equipment = result;
    });

    this.equipmentService.getEnhanceCost().subscribe(result =>
    {
      this.costs = result;
    });

    this.updateBalance();
  }

  private updateBalance()
  {
    this.inventoryService.getBalance().subscribe(result =>
    {
      this.balance = result;
    });
  }

  ngOnInit()
  {
  }

  public enhance(): void
  {
    this.enhancementInProgress = true;
    this.equipmentService.enhance(this.equipment).subscribe(result =>
    {
      this.enhancementInProgress = false;
      this.equipment = result;
      this.updateBalance();
    }, error =>
    {
      this.enhancementInProgress = false;
    });
  }
}
