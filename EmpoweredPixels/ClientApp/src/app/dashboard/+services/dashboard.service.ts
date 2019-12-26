import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DashboardLeague } from '../+models/dashboard-league';
import { DashboardFighter } from '../+models/dashboard-fighter';
import { DashboardFighterResult } from '../+models/dashboard-fighter-result';
import { DashboardSeason } from '../+models/dashboard-season';

@Injectable({
  providedIn: 'root'
})
export class DashboardService
{

  constructor(private http: HttpClient) { }

  public getLeagues(): Observable<DashboardLeague[]>
  {
    return this.http.get<DashboardLeague[]>('api/dashboard/leagues');
  }

  public getFighters(): Observable<DashboardFighter[]>
  {
    return this.http.get<DashboardFighter[]>('api/dashboard/fighters');
  }

  public getFighterResults(fighterId: string): Observable<DashboardFighterResult[]>
  {
    return this.http.get<DashboardFighterResult[]>(`api/dashboard/results/${fighterId}`);
  }

  public getSeason(): Observable<DashboardSeason>
  {
    return this.http.get<DashboardSeason>('api/dashboard/season');
  }
}
