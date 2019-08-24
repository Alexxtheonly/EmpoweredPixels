import { Item } from './../+models/item';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Reward } from '../+models/reward';

@Injectable({
  providedIn: 'root'
})
export class RewardService
{

  constructor(private http: HttpClient) { }

  public getRewards(): Observable<Reward[]>
  {
    return this.http.get<Reward[]>('api/reward');
  }

  public claim(reward: Reward): Observable<Item[]>
  {
    return this.http.post<Item[]>('api/reward/claim', reward);
  }
}
