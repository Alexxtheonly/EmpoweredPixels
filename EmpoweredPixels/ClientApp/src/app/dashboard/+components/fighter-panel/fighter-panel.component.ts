import { DashboardService } from './../../+services/dashboard.service';
import { DashboardFighter } from './../../+models/dashboard-fighter';
import { Observable } from 'rxjs';
import { RosterService } from './../../../roster/+services/roster.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-fighter-panel',
  templateUrl: './fighter-panel.component.html',
  styleUrls: ['./fighter-panel.component.css']
})
export class FighterPanelComponent implements OnInit
{
  public fighters$: Observable<DashboardFighter[]>;

  constructor(dashboardService: DashboardService)
  {
    this.fighters$ = dashboardService.getFighters();
  }

  ngOnInit()
  {
  }

}
