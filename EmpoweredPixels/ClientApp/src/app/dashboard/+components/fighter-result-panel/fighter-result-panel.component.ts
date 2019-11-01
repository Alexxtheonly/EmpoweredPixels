import { DashboardFighterResult } from './../../+models/dashboard-fighter-result';
import { Observable } from 'rxjs';
import { DashboardService } from './../../+services/dashboard.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-fighter-result-panel',
  templateUrl: './fighter-result-panel.component.html',
  styleUrls: ['./fighter-result-panel.component.css']
})
export class FighterResultPanelComponent implements OnInit
{
  public results$: Observable<DashboardFighterResult[]>;

  @Input()
  set fighterId(fighterId: string)
  {
    this.results$ = this.dashboardService.getFighterResults(fighterId);
  }

  constructor(private dashboardService: DashboardService) { }

  ngOnInit()
  {
  }

}
