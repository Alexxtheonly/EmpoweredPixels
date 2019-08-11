import { LeagueDetail } from './../+models/league-detail';
import { LeagueSubscription } from './../+models/league-subscription';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { League } from '../+models/league';
import { LeagueMatch } from '../+models/league-match';

@Injectable({
  providedIn: 'root'
})
export class LeagueService
{

  constructor(private http: HttpClient) { }

  public getLeagues(): Observable<League[]>
  {
    return this.http.get<League[]>('api/league');
  }

  public getLeague(id: number): Observable<LeagueDetail>
  {
    return this.http.get<LeagueDetail>(`api/league/${id}`);
  }

  public subscribeLeague(leagueId: number, fighterId: string): Observable<any>
  {
    const subscription = new LeagueSubscription();
    subscription.fighterId = fighterId;
    subscription.leagueId = leagueId;

    return this.http.post('api/league/subscribe', subscription);
  }

  public unsubscribeLeague(leagueId: number, fighterId: string): Observable<any>
  {
    const subscription = new LeagueSubscription();
    subscription.fighterId = fighterId;
    subscription.leagueId = leagueId;

    return this.http.post('api/league/unsubscribe', subscription);
  }

  public getSubscriptions(leagueId: number): Observable<LeagueSubscription[]>
  {
    return this.http.get<LeagueSubscription[]>(`api/league/${leagueId}`);
  }

  public getUserSubscriptions(leagueId: number): Observable<LeagueSubscription[]>
  {
    return this.http.get<LeagueSubscription[]>(`api/league/${leagueId}/user`);
  }

  public getLeagueMatches(leagueId: number): Observable<LeagueMatch[]>
  {
    return this.http.get<LeagueMatch[]>(`api/league/${leagueId}/matches`);
  }
}
