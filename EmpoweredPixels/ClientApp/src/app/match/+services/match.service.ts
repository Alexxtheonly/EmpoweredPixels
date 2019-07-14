import { MatchRegistration } from '../+models/match-registration';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Match } from '../+models/match';
import { MatchResult } from 'src/app/match-viewer/+models/match-result';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private http: HttpClient) { }

  public createMatch(): Observable<Match> {
    return this.http.put<Match>('api/match', null);
  }

  public getMatch(id: string): Observable<Match> {
    return this.http.get<Match>(`api/match/${id}`);
  }

  public startMatch(match: Match): Observable<any> {
    return this.http.post('api/match/start', match);
  }

  public getMatchResult(id: string): Observable<MatchResult> {
    return this.http.get<MatchResult>(`api/match/${id}/result`);
  }

  public joinMatch(registration: MatchRegistration): Observable<any> {
    return this.http.post('api/match/join', registration);
  }

  public leaveMatch(registration: MatchRegistration): Observable<any> {
    return this.http.post('api/match/leave', registration);
  }
}
