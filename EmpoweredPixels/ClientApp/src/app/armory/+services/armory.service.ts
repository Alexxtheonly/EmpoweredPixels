import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FighterArmory } from '../+models/fighter-armory';

@Injectable({
  providedIn: 'root'
})
export class ArmoryService
{

  constructor(private http: HttpClient)
  {
  }

  public getFighterArmory(fighterId: string): Observable<FighterArmory>
  {
    return this.http.get<FighterArmory>(`api/armory/${fighterId}`);
  }
}
