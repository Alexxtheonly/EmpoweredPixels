import { ShopFilter } from './../+models/shop-filter';
import { Page } from './../../match/+models/page';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShopItem } from '../+models/shop-item';
import { RewardContent } from 'src/app/rewards/+models/reward-content';

@Injectable({
  providedIn: 'root'
})
export class ShopService
{

  constructor(private http: HttpClient) { }

  public getShopContent(shopFilter: ShopFilter): Observable<Page<ShopItem>>
  {
    return this.http.post<Page<ShopItem>>('api/shop', shopFilter);
  }

  public buyItem(shopItem: ShopItem): Observable<RewardContent>
  {
    return this.http.post<RewardContent>('api/shop/buy', shopItem);
  }
}
