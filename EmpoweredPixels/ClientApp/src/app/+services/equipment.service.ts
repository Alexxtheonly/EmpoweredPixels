import { PagingOptions } from 'src/app/match/+models/paging-options';
import { EquipmentFilter } from './../+models/equipment-filter';
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
  public getEnhanceCost(): Observable<number>
  {
    return this.http.get<number>('api/equipment/enhance/cost');
  }

  public enhance(equipment: Equipment): Observable<Equipment>
  {
    return this.http.post<Equipment>('api/equipment/enhance', equipment);
  }

  public salvage(equipment: Equipment): Observable<Item[]>
  {
    return this.http.post<Item[]>('api/equipment/salvage', equipment);
  }

  public getInventoryPage(options: PagingOptions): Observable<Page<Equipment>>
  {
    return this.http.post<Page<Equipment>>('api/equipment/inventory', options);
  }
}
