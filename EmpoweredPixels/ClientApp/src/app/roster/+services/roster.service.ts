import { Fighter } from './../+models/fighter';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RosterService {

  constructor(private http: HttpClient) { }

  public getFighters(): Observable<Fighter[]> {
    return this.http.get<Fighter[]>('api/fighter');
  }

  public getFighter(id: string): Observable<Fighter> {
    return this.http.get<Fighter>(`api/fighter/${id}`);
  }

  public createFighter(fighter: Fighter): Observable<Fighter> {
    return this.http.put<Fighter>('api/fighter', fighter);
  }

  public updateFighter(fighter: Fighter): Observable<Fighter> {
    return this.http.post<Fighter>('api/fighter', fighter);
  }
}