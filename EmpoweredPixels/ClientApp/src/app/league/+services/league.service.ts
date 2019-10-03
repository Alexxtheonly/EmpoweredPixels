import { LeagueHighscoreOptions } from './../+models/league-highscore-options';
import { PagingOptions } from './../../match/+models/paging-options';
import { LeagueDetail } from './../+models/league-detail';
import { LeagueSubscription } from './../+models/league-subscription';
import { Observable, observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { League } from '../+models/league';
import { LeagueMatch } from '../+models/league-match';
import { Page } from 'src/app/match/+models/page';
import { LeagueHighscore } from '../+models/league-highscore';
import { LeagueMatchWinner } from '../+models/league-match-winner';

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
    return this.http.get<LeagueSubscription[]>(`api/league/${leagueId}/subscriptions/user`);
  }

  public getLeagueMatches(leagueId: number, pagingOptions: PagingOptions): Observable<Page<LeagueMatch>>
  {
    return this.http.post<Page<LeagueMatch>>(`api/league/${leagueId}/matches`, pagingOptions);
  }

  public getLeagueHighscores(leagueId: number, options: LeagueHighscoreOptions): Observable<LeagueHighscore[]>
  {
    return this.http.post<LeagueHighscore[]>(`api/league/${leagueId}/highscores`, options);
  }

  public getLastLeagueWinner(leagueId: number): Observable<LeagueMatchWinner>
  {
    return this.http.get<LeagueMatchWinner>(`api/league/${leagueId}/winner`);
  }
}
