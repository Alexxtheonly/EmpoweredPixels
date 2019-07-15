import { MatchResult } from '../+models/match-result';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatchViewerService {
  public match: MatchResult;

  constructor(public http: HttpClient) {
  }

  public getTestmatch(): Observable<MatchResult> {
    return this.http.get<MatchResult>('api/match/test');
  }
}
