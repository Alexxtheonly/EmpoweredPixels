import { Item } from './../../+models/item';
import { RewardService } from './../../+services/reward.service';
import { Reward } from './../../+models/reward';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-reward',
  templateUrl: './reward.component.html',
  styleUrls: ['./reward.component.css']
})
export class RewardComponent implements OnInit
{
  @Input()
  public reward: Reward;

  public loading: boolean;

  public items: Item[];

  public showModal: boolean;

  public claimed: boolean;

  constructor(private rewardService: RewardService) { }

  ngOnInit()
  {
  }

  public claim(): void
  {
    this.loading = true;
    this.rewardService.claim(this.reward).subscribe(result =>
    {
      this.loading = false;
      this.items = result;
      this.showModal = true;
      this.claimed = true;
    });
  }

}
