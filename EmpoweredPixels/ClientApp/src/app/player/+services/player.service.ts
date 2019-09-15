import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PlayerExperience } from '../+models/player-experience';

@Injectable({
  providedIn: 'root'
})
export class PlayerService
{

  constructor(private http: HttpClient) { }

  public getPlayerExperience(): Observable<PlayerExperience>
  {
    return this.http.get<PlayerExperience>('api/player/experience');
  }
}
