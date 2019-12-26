import { DashboardService } from './../../+services/dashboard.service';
import { DashboardSeason } from './../../+models/dashboard-season';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-season-panel',
  templateUrl: './season-panel.component.html',
  styleUrls: ['./season-panel.component.css']
})
export class SeasonPanelComponent implements OnInit
{
  public season$: Observable<DashboardSeason>;

  constructor(dashboardService: DashboardService, private translate: TranslateService)
  {
    this.season$ = dashboardService.getSeason();
  }

  ngOnInit()
  {
    moment.locale(this.translate.getBrowserLang());
  }

  public getDiff(date: Date): string
  {
    return moment(date).fromNow();
  }

  public getSeasonProgress(season: DashboardSeason): number
  {
    const remaining = moment(season.endDate).diff(moment(), 'hours');
    const total = moment(season.endDate).diff(moment(season.startDate), 'hours');

    return ((total - remaining) / total) * 100;
  }
}
