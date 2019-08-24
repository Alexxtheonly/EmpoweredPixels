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

  constructor(private rewardService: RewardService)
  {
    this.rewards = this.rewardService.getRewards();
  }

  ngOnInit()
  {
  }

}
