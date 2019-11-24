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

  public stats: EnhancementStats = new EnhancementStats();

  public desiredEnhancement = 0;

  public autoEnhancementInProgress = false;

  constructor(private route: ActivatedRoute, private equipmentService: EquipmentService, private inventoryService: InventoryService)
  {
  }

  private async updateBalance()
  {
    this.balance = await this.inventoryService.getBalance().toPromise();
  }

  async ngOnInit()
  {
    const id: string = this.route.snapshot.paramMap.get('id');

    this.equipment = await this.equipmentService.getEquipment(id).toPromise();

    this.costs = await this.equipmentService.getEnhanceCost().toPromise();

    await this.updateBalance();
  }

  public async enhance(): Promise<void>
  {
    this.enhancementInProgress = true;
    const old = this.equipment;
    try
    {
      this.equipment = await this.equipmentService.enhance(this.equipment).toPromise();
    }
    catch
    {
      this.enhancementInProgress = false;
      return;
    }
    this.enhancementInProgress = false;
    await this.updateBalance();

    this.stats.tries++;
    if (old.enhancement < this.equipment.enhancement)
    {
      this.handleSuccess();
    } else
    {
      this.handleFailure();
    }
  }

  public async autoEnhance()
  {
    this.autoEnhancementInProgress = true;
    while (this.equipment.enhancement < this.desiredEnhancement)
    {
      await this.enhance();
      if (this.balance.balance < this.costs)
      {
        this.desiredEnhancement = 0;
      }
    }
    this.autoEnhancementInProgress = false;
  }

  private handleSuccess()
  {
    if (this.equipment.enhancement > this.stats.maxEnhancement)
    {
      this.stats.maxEnhancement = this.equipment.enhancement;
    }
  }

  private handleFailure()
  {
    this.stats.failures++;
  }
}
