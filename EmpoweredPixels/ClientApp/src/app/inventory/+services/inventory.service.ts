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

  public getParticleBalance(): Observable<CurrencyBalance>
  {
    return this.http.get<CurrencyBalance>('api/inventory/balance/particles');
  }

  public getCommonTokenBalance(): Observable<CurrencyBalance>
  {
    return this.http.get<CurrencyBalance>('api/inventory/balance/token/common');
  }

  public getRareTokenBalance(): Observable<CurrencyBalance>
  {
    return this.http.get<CurrencyBalance>('api/inventory/balance/token/rare');
  }

  public getFabledTokenBalance(): Observable<CurrencyBalance>
  {
    return this.http.get<CurrencyBalance>('api/inventory/balance/token/fabled');
  }

  public getMythicTokenBalance(): Observable<CurrencyBalance>
  {
    return this.http.get<CurrencyBalance>('api/inventory/balance/token/mythic');
  }
}
