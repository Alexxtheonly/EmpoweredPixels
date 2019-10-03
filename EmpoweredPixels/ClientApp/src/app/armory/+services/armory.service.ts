import { FighterArmoryOverview } from './../+models/fighter-armory-overview';
import { PagingOptions } from './../../match/+models/paging-options';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FighterArmory } from '../+models/fighter-armory';
import { Page } from 'src/app/match/+models/page';

@Injectable({
  providedIn: 'root'
})
export class ArmoryService
{

  constructor(private http: HttpClient)
  {
  }

  public getFighterArmoryOverview(options: PagingOptions): Observable<Page<FighterArmoryOverview>>
  {
    return this.http.post<Page<FighterArmoryOverview>>('api/armory', options);
  }

  public getFighterArmory(fighterId: string): Observable<FighterArmory>
  {
    return this.http.get<FighterArmory>(`api/armory/${fighterId}`);
  }
}
