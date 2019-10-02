import { RewardContent } from './../../+models/reward-content';
import { Equipment } from './../../../roster/+models/equipment';
import { UserFeedbackService } from './../../../+services/userfeedback.service';
import { Item } from './../../+models/item';
import { RewardService } from './../../+services/reward.service';
import { Reward } from './../../+models/reward';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

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

  public showModal: boolean;

  public claimed: boolean;

  @Output()
  rewardClaimed = new EventEmitter<RewardContent>();

  constructor(private rewardService: RewardService, private userfeedbackService: UserFeedbackService) { }

  ngOnInit()
  {
  }

  public claim(): void
  {
    this.loading = true;
    this.rewardService.claim(this.reward).subscribe(result =>
    {
      this.loading = false;
      this.rewardClaimed.emit(result);
      this.showModal = true;
      this.claimed = true;
    }, error =>
    {
      this.loading = false;
      this.userfeedbackService.error(error);
    });
  }

  public getCssRarity(): string
  {
    if (!this.reward)
    {
      return '';
    }

    switch (this.reward.poolId)
    {
      case '6c70ddab-5b5c-4b1e-849f-78ceb7d14751':
        return 'rarity-common';
      case 'e620ff6f-e081-4588-b1e1-652f06808359':
        return 'rarity-rare';
      case 'd00258c4-cb35-4ab3-bd00-bdb356bb6c2c':
        return 'rarity-fabled';
      case 'b051e5c9-a679-489f-95c6-4e32aed2d15b':
        return 'rarity-mythic';
      default:
        return 'rarity-basic';
    }
  }

}
