import { Enhancement } from './../+models/enhancement';
import { EnhancementStats } from './enhancement-stats';
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

  public enhancement: Enhancement = new Enhancement();

  constructor(private route: ActivatedRoute, private equipmentService: EquipmentService, private inventoryService: InventoryService)
  {
  }

  private async updateBalance()
  {
    this.balance = await this.inventoryService.getParticleBalance().toPromise();
  }

  async ngOnInit()
  {
    const id: string = this.route.snapshot.paramMap.get('id');

    this.equipment = await this.equipmentService.getEquipment(id).toPromise();

    this.enhancement.equipment = this.equipment;

    await this.updateCosts();

    await this.updateBalance();
  }

  public async updateCosts()
  {
    this.costs = await this.equipmentService.getEnhanceCost(this.enhancement).toPromise();
  }

  public async enhance(): Promise<void>
  {
    this.enhancementInProgress = true;
    try
    {
      this.equipment = await this.equipmentService.enhance(this.enhancement).toPromise();
    }
    catch
    {
      this.enhancementInProgress = false;
      return;
    }
    this.enhancementInProgress = false;

    await this.updateCosts();
    await this.updateBalance();
  }
}
