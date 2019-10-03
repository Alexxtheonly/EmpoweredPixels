import { AuthService } from './../../auth/auth.service';
import { ArmoryService } from './../+services/armory.service';
import { PagingOptions } from 'src/app/match/+models/paging-options';
import { Component, OnInit } from '@angular/core';
import { Page } from 'src/app/match/+models/page';
import { FighterArmoryOverview } from '../+models/fighter-armory-overview';

@Component({
  selector: 'app-armory-overview',
  templateUrl: './armory-overview.component.html',
  styleUrls: ['./armory-overview.component.css']
})
export class ArmoryOverviewComponent implements OnInit
{
  public options: PagingOptions = new PagingOptions();

  public loading: boolean;

  public page: Page<FighterArmoryOverview>;

  public userId: number;

  constructor(private armoryService: ArmoryService, authService: AuthService)
  {
    this.userId = authService.getUserId();
    this.loadArmoryOverview();
  }

  ngOnInit()
  {
  }

  public loadArmoryOverview()
  {
    this.armoryService.getFighterArmoryOverview(this.options).subscribe(result =>
    {
      this.page = result;
      this.loading = false;
    });
  }

  public loadPage(page: number)
  {
    this.loading = true;
    this.options.pageNumber = page;
    this.loadArmoryOverview();
  }

}
