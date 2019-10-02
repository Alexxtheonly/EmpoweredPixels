import { RewardContent } from './../+models/reward-content';
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

  public claim(reward: Reward): Observable<RewardContent>
  {
    return this.http.post<RewardContent>('api/reward/claim', reward);
  }

  public claimAll(): Observable<RewardContent>
  {
    return this.http.post<RewardContent>('api/reward/claim/all', null);
  }
}
