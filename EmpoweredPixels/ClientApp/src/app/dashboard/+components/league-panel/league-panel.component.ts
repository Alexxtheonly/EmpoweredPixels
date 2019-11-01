import { DashboardService } from './../../+services/dashboard.service';
import { Observable } from 'rxjs';
import { DashboardLeague } from './../../+models/dashboard-league';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-league-panel',
  templateUrl: './league-panel.component.html',
  styleUrls: ['./league-panel.component.css']
})
export class LeaguePanelComponent implements OnInit
{

  public leagues$: Observable<DashboardLeague[]>;

  constructor(dashboardService: DashboardService)
  {
    this.leagues$ = dashboardService.getLeagues();
  }

  ngOnInit()
  {
  }

}
