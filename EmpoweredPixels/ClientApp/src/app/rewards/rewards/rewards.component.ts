import { RewardContent } from './../+models/reward-content';
import { Observable } from 'rxjs';
import { Reward } from './../+models/reward';
import { RewardService } from './../+services/reward.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rewards',
  templateUrl: './rewards.component.html',
  styleUrls: ['./rewards.component.css']
})
export class RewardsComponent implements OnInit
{
  public rewards: Observable<Reward[]>;

  public rewardContent: RewardContent;

  public showModal: boolean;

  constructor(private rewardService: RewardService)
  {
    this.loadRewards();
  }

  private loadRewards()
  {
    this.rewards = this.rewardService.getRewards();
  }

  ngOnInit()
  {
  }

  public setRewardContent(rewardContent: RewardContent): void
  {
    this.rewardContent = rewardContent;
    this.showModal = true;
  }

  public claimAll(): void
  {
    this.rewardService.claimAll().subscribe(result =>
    {
      this.setRewardContent(result);
      this.loadRewards();
    });
  }
}
