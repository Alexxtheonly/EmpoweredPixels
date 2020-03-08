import { Enhancement } from './../inventory/+models/enhancement';
import { PagingOptions } from 'src/app/match/+models/paging-options';
import { Item } from './../rewards/+models/item';
import { Equipment } from './../roster/+models/equipment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Page } from '../match/+models/page';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService
{

  constructor(private http: HttpClient) { }

  public getEquipment(id: string): Observable<Equipment>
  {
    return this.http.get<Equipment>(`api/equipment/${id}`);
  }
  public getEnhanceCost(enhancement: Enhancement): Observable<number>
  {
    return this.http.post<number>('api/equipment/enhance/cost', enhancement);
  }

  public enhance(enhancement: Enhancement): Observable<Equipment>
  {
    return this.http.post<Equipment>('api/equipment/enhance', enhancement);
  }

  public salvage(equipment: Equipment): Observable<Item[]>
  {
    return this.http.post<Item[]>('api/equipment/salvage', equipment);
  }

  public salvageInventory(): Observable<Item[]>
  {
    return this.http.post<Item[]>('api/equipment/salvage/inventory', null);
  }

  public setFavorite(equipmentId: string): Observable<Equipment>
  {
    return this.http.post<Equipment>(`api/equipment/${equipmentId}/favorite`, null);
  }

  public unsetFavorite(equipmentId: string): Observable<Equipment>
  {
    return this.http.delete<Equipment>(`api/equipment/${equipmentId}/favorite`);
  }

  public getInventoryPage(options: PagingOptions): Observable<Page<Equipment>>
  {
    return this.http.post<Page<Equipment>>('api/equipment/inventory', options);
  }
}
