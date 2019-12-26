import { CurrencyBalance } from 'src/app/inventory/+models/currency-balance';
import { Page } from './../match/+models/page';
import { SeasonSummary } from './+models/season-summary';
import { SeasonService } from './+services/season.service';
import { PagingOptions } from 'src/app/match/+models/paging-options';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-season',
  templateUrl: './season.component.html',
  styleUrls: ['./season.component.css']
})
export class SeasonComponent implements OnInit
{
  public options: PagingOptions = new PagingOptions();

  public loading: boolean;

  public page: Page<SeasonSummary>;

  constructor(private seasonService: SeasonService) { }

  ngOnInit()
  {
    this.loadSeasonSummary()
  }

  public loadPage(page: number)
  {
    this.loading = true;
    this.options.pageNumber = page;
    this.loadSeasonSummary();
  }

  public async loadSeasonSummary()
  {
    this.page = await this.seasonService.getSummaryPage(this.options).toPromise();
    this.loading = false;
  }

  public getBonusBalance(summary: SeasonSummary)
  {
    return this.getBalance(summary.bonus);
  }

  public getSalvageBalance(summary: SeasonSummary)
  {
    return this.getBalance(summary.salvageValue);
  }

  private getBalance(value: number)
  {
    const balance = new CurrencyBalance();
    balance.balance = value;
    balance.itemId = 'a1fe94ec-5b54-4c1b-a5c0-0439f4a7e702';

    return balance;
  }
}
