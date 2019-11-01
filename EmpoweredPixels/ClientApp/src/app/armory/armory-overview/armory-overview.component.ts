import { AuthService } from './../../auth/auth.service';
import { ArmoryService } from './../+services/armory.service';
import { PagingOptions } from 'src/app/match/+models/paging-options';
import { Component, OnInit } from '@angular/core';
import { Page } from 'src/app/match/+models/page';
import { FighterArmoryOverview } from '../+models/fighter-armory-overview';
import { Router, ActivatedRoute } from '@angular/router';

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

  constructor(
    private armoryService: ArmoryService,
    authService: AuthService,
    private router: Router,
    private route: ActivatedRoute)
  {
    this.userId = authService.getUserId();

    this.route.queryParams.subscribe(result =>
    {
      let page = result['page'];

      if (!page)
      {
        page = 1;
      }

      this.loadPage(page);
    });
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
      this.router.navigate(
        [],
        {
          relativeTo: this.route,
          queryParams: { page: this.options.pageNumber },
        });
    });
  }

  public loadPage(page: number)
  {
    this.loading = true;
    this.options.pageNumber = page;
    this.loadArmoryOverview();
  }

}
