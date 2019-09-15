import { PlayerExperience } from './../player/+models/player-experience';
import { InventoryService } from './../inventory/+services/inventory.service';
import { PlayerService } from './../player/+services/player.service';
import { RewardService } from './../rewards/+services/reward.service';
import { Component, OnInit } from '@angular/core';
import { CurrencyBalance } from '../inventory/+models/currency-balance';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit
{
  public rewardCount: number;
  public balance: CurrencyBalance;
  public experience: PlayerExperience;

  isExpanded = false;

  constructor(private rewardService: RewardService, private playerService: PlayerService, private inventoryService: InventoryService)
  {
  }

  ngOnInit(): void
  {
    this.rewardService.getRewards().subscribe(result =>
    {
      this.rewardCount = result.length;
    });

    this.playerService.getPlayerExperience().subscribe(result =>
    {
      this.experience = result;
    });

    this.inventoryService.getBalance().subscribe(result =>
    {
      this.balance = result;
    });
  }

  collapse()
  {
    this.isExpanded = false;
  }

  toggle()
  {
    this.isExpanded = !this.isExpanded;
  }
}
