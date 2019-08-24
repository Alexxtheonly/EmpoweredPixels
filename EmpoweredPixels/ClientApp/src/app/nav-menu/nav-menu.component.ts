import { RewardService } from './../rewards/+services/reward.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit
{
  public rewardCount: number;

  isExpanded = false;

  constructor(private rewardService: RewardService)
  {
  }

  ngOnInit(): void
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
