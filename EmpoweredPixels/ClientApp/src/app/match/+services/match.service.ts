import { Fighter } from './../../roster/+models/fighter';
import { MatchTeam } from './../+models/match-team';
import { Match } from './../+models/match';
import { PagingOptions } from './../+models/paging-options';
import { MatchRegistration } from '../+models/match-registration';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MatchResult } from 'src/app/match-viewer/+models/match-result';
import { MatchOptions } from '../+models/match-options';
import { Page } from '../+models/page';
import { MatchTeamOperation } from '../+models/match-team-operation';

@Injectable({
  providedIn: 'root'
})
export class MatchService
{

  constructor(private http: HttpClient) { }

  public createMatch(matchOptions: MatchOptions): Observable<Match>
  {
    return this.http.put<Match>('api/match/create', matchOptions);
  }

  public getDefaultMatchOptions(): Observable<MatchOptions>
  {
    return this.http.get<MatchOptions>('api/match/options/default');
  }

  public getAvailableSizes(): Observable<Array<string>>
  {
    return this.http.get<Array<string>>('api/match/options/sizes');
  }

  public getMatch(id: string): Observable<Match>
  {
    return this.http.get<Match>(`api/match/${id}`);
  }

  public startMatch(match: Match): Observable<any>
  {
    return this.http.post('api/match/start', match);
  }

  public getMatchResult(id: string): Observable<MatchResult>
  {
    return this.http.get<MatchResult>(`api/match/${id}/result`);
  }

  public joinMatch(registration: MatchRegistration): Observable<any>
  {
    return this.http.post('api/match/join', registration);
  }

  public leaveMatch(registration: MatchRegistration): Observable<any>
  {
    return this.http.post('api/match/leave', registration);
  }

  public browse(options: PagingOptions): Observable<Page<Match>>
  {
    return this.http.post<Page<Match>>('api/match/browse', options);
  }

  public createTeam(match: Match, password?: string): Observable<MatchTeam>
  {
    const operation = new MatchTeamOperation();
    operation.matchId = match.id;
    operation.password = password;

    return this.http.put<MatchTeam>('api/match/create/team', operation);
  }

  public getTeams(match: Match): Observable<MatchTeam[]>
  {
    return this.http.get<MatchTeam[]>(`api/match/${match.id}/teams`);
  }

  public joinTeam(match: Match, teamId: string, fighterId: string, password?: string): Observable<any>
  {
    const operation = new MatchTeamOperation();
    operation.id = teamId;
    operation.matchId = match.id;
    operation.password = password;
    operation.fighterId = fighterId;

    return this.http.post('api/match/join/team', operation);
  }

  public leaveTeam(match: Match, fighterId: string): Observable<any>
  {
    const operation = new MatchTeamOperation();
    operation.matchId = match.id;
    operation.fighterId = fighterId;

    return this.http.post('api/match/leave/team', operation);
  }
}
