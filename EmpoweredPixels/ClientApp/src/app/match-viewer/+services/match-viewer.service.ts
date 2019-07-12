import { Match } from './../+models/match';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatchViewerService {
  public match: Match;

  constructor(public http: HttpClient) {
  }

  public getTestmatch(): Observable<Match> {
    return this.http.get<Match>('api/match/test');
  }
}
