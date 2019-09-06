import { FighterName } from './../../match-viewer/+models/fighter-name';
import { Fighter } from './../+models/fighter';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FighterStatForecast } from '../+models/fighter-stat-forecast';

@Injectable({
  providedIn: 'root'
})
export class RosterService
{

  constructor(private http: HttpClient) { }

  public getFighters(): Observable<Fighter[]>
  {
    return this.http.get<Fighter[]>('api/fighter');
  }

  public getFighter(id: string): Observable<Fighter>
  {
    return this.http.get<Fighter>(`api/fighter/${id}`);
  }

  public getFighterName(id: string): Observable<FighterName>
  {
    return this.http.get<FighterName>(`api/fighter/${id}/name`);
  }

  public createFighter(fighter: Fighter): Observable<Fighter>
  {
    return this.http.put<Fighter>('api/fighter', fighter);
  }

  public updateFighter(fighter: Fighter): Observable<Fighter>
  {
    return this.http.post<Fighter>('api/fighter', fighter);
  }

  public getFighterStatForecast(fighter: Fighter): Observable<FighterStatForecast>
  {
    return this.http.post<FighterStatForecast>('api/fighter/forecast', fighter);
  }
}
