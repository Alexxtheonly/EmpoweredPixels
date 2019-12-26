import { Observable } from 'rxjs';
import { PagingOptions } from 'src/app/match/+models/paging-options';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Page } from 'src/app/match/+models/page';
import { SeasonSummary } from '../+models/season-summary';

@Injectable({
  providedIn: 'root'
})
export class SeasonService
{

  constructor(private http: HttpClient) { }

  public getSummaryPage(options: PagingOptions): Observable<Page<SeasonSummary>>
  {
    return this.http.post<Page<SeasonSummary>>('api/season/summary', options);
  }
}
