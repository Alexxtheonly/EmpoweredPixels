import { PlayerExperience } from './../player/+models/player-experience';
import { RewardService } from './../rewards/+services/reward.service';
import { Component, OnInit } from '@angular/core';
import { CurrencyBalance } from '../inventory/+models/currency-balance';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit
{
  public rewardCount: number;

  isExpanded = false;

  constructor(
    private rewardService: RewardService,
    private router: Router)
  {
    router.events.subscribe((event) =>
    {
      if (event instanceof NavigationEnd)
      {
        this.loadRewards();
      }
    });
  }

  ngOnInit(): void
  {
    this.loadRewards();
  }

  private loadRewards()
  {
    this.rewardService.getRewards().subscribe(result =>
    {
      this.rewardCount = result.length;
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
