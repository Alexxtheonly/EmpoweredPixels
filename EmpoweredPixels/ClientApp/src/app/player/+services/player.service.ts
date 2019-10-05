import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FighterExperience } from '../+models/fighter-experience';

@Injectable({
  providedIn: 'root'
})
export class PlayerService
{

  constructor(private http: HttpClient) { }

  public getPlayerExperience(): Observable<FighterExperience>
  {
    return this.http.get<FighterExperience>('api/player/experience');
  }
}
