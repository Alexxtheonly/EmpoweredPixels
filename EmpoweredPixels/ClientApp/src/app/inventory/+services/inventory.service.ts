import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CurrencyBalance } from '../+models/currency-balance';

@Injectable({
  providedIn: 'root'
})
export class InventoryService
{

  constructor(private http: HttpClient) { }

  public getBalance(): Observable<CurrencyBalance>
  {
    return this.http.get<CurrencyBalance>('api/inventory/balance');
  }
}
